using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using EntityGraphQL.AspNet;
using EntityGraphQL.Extensions;
using EntityGraphQL.Schema;
using JCystems.GettingStarted.Connectors.Contexts;
using JCystems.GettingStarted.Connectors.Data;
using JCystems.GettingStarted.Connectors.Services;
using JCystems.GettingStarted.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.env.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.Services
            .AddOptions<ServiceOptions>()
            .BindConfiguration(ServiceOptions.Key);
        //.ValidateFluently() // TODO
        //.ValidateOnStart(); // TODO

        var provider = builder.Services.BuildServiceProvider();
        var optionsMonitor = provider.GetRequiredService<IOptionsMonitor<ServiceOptions>>();

        ServiceOptions config;
        config = provider.GetRequiredService<IOptionsMonitor<ServiceOptions>>().CurrentValue;

        builder.Services
            .AddOpenApi(options =>
            {
                // Specify the OpenAPI version to use
                options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
            })
            .AddDbContext<DemoContext>(options => options.UseSqlServer(config.Database.ReadConnectionString))
            .AddGraphQLSchema<DemoContext>()
            .AddGraphQLValidator() // Add validation support for mutations
            .AddSingleton<AgeService>() // Add services that will be injected into GraphQL fields

            // >>> WARNING <<< This thing breaks
            // "Cannot find 'Movies' in 'Query'
            // .AddSingleton<SchemaProvider<DemoContext>>() // FIXME

            .AddControllers() // Option C: Custom controllers
                .AddJsonOptions(opts =>
                {
                    var converter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(converter); // Use enum field names instead of numbers
                    opts.JsonSerializerOptions.IncludeFields = true; // EntityGraphQL internally builds types with fields

                    // The fields internally built already are named with fieldNamer (defaults to camelCase). This is
                    // for the properties on QueryResult (Data, Errors) to match what most tools etc expect (camelCase)
                    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
        ;

        var app = builder.Build();
        CreateDbIfNotExists(app.Services);

        app.UseRouting();
        app.UseStaticFiles(); // Serve static files (GraphiQL UI)
        app.MapFallbackToFile("index.html");

        app.MapGet("/health", () => "Healthy!");
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // Option A
        app.MapGraphQL<DemoContext>(followSpec: true, options: new ExecutionOptions
        {
            // Add EF Core query tags for debugging
            BeforeRootFieldExpressionBuild = (exp, op, field) =>
            {
                if (exp.Type.IsGenericTypeQueryable())
                    return Expression.Call(
                        typeof(EntityFrameworkQueryableExtensions),
                        nameof(EntityFrameworkQueryableExtensions.TagWith),
                        [exp.Type.GetGenericArguments()[0]],
                        exp,
                        Expression.Constant($"GQL op: {op ?? "n/a"}, field: {field}")
                    );
                return exp;
            },

            // TODO? master
            // IncludeDebugInfo = true,

            IncludeQueryInfo = true, // Include query execution metadata
        });

        // Option B
        //app.UseEndpoints(endpoints =>
        //{
        //    // defaults to /graphql endpoint
        //    endpoints.MapGraphQL<DemoContext>(configureEndpoint: (endpoint) =>
        //    {
        //        // endpoint.RequireAuthorization("authorized");
        //        // do other things with endpoint
        //    });
        //});

        app.Run();
    }

    private static void CreateDbIfNotExists(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;

            var context = scopedServices.GetRequiredService<DemoContext>();
            DbInitializer.Initialize(context);
        }
    }
}
