using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using EntityGraphQL.AspNet;
using EntityGraphQL.Extensions;
using EntityGraphQL.Schema;
using JCystems.GettingStarted.Connectors.Contexts;
using JCystems.GettingStarted.Connectors.Data;
using JCystems.GettingStarted.Connectors.Services;
using JCystems.GettingStarted.GraphQL;
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
            .AddOpenApi(oOpenApiOptions =>
            {
                // Specify the OpenAPI version to use
                oOpenApiOptions.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
            })
            .AddDbContext<DemoContext>(oAddDbContext => oAddDbContext.UseSqlServer(config.Database.ReadConnectionString))
            .AddGraphQLSchema<DemoContext>(oAddGraphQLOptions =>
            {
                oAddGraphQLOptions.PreBuildSchemaFromContext = schema =>
                {
                    // add in needed mappings for our context
                    schema.AddScalarType<KeyValuePair<string, string>>("StringKeyValuePair", "Represents a pair of strings");
                };

                oAddGraphQLOptions.ConfigureSchema = GraphQLOptions.ConfigureSchema;
                // below this will generate the field names as they are from the reflected dotnet types - i.e matching the case
                // builder.FieldNamer = name => name;
            })
            .AddGraphQLValidator() // Add validation support for mutations
            .AddSingleton<AgeService>() // Add services that will be injected into GraphQL fields

            // >>> WARNING <<< This thing breaks
            // "Cannot find 'Movies' in 'Query'
            // .AddTransient<SchemaProvider<DemoContext>>()

            .AddControllers() // Option C: Custom controllers
                .AddJsonOptions(oJsonOptions =>
                {
                    var converter = new JsonStringEnumConverter();
                    oJsonOptions.JsonSerializerOptions.Converters.Add(converter); // Use enum field names instead of numbers
                    oJsonOptions.JsonSerializerOptions.IncludeFields = true; // EntityGraphQL internally builds types with fields

                    // The fields internally built already are named with fieldNamer (defaults to camelCase). This is
                    // for the properties on QueryResult (Data, Errors) to match what most tools etc expect (camelCase)
                    oJsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
        ;

        var app = builder.Build();
        CreateDbIfNotExists(app.Services);

        app.UseRouting();
        app.UseStaticFiles(); // Serve static files (GraphiQL UI)

        app.MapFallbackToFile("index.html"); // XXX DELETE?
        // app.UseDefaultFiles(); // TODO?

        app.MapGet("/health", () => "Healthy!");
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // Option A
        app.MapControllers();
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
        //        app.UseEndpoints(endpoints =>
        //        {
        //            endpoints.MapControllers();
        //            endpoints.MapGraphQL<DemoContext>(
        //                oOpenApiOptions: new ExecutionOptions
        //                {
        //                    BeforeRootFieldExpressionBuild = (exp, op, field) =>
        //                    {
        //                        if (exp.Type.IsGenericTypeQueryable())
        //                            return Expression.Call(
        //                                typeof(EntityFrameworkQueryableExtensions),
        //                                nameof(EntityFrameworkQueryableExtensions.TagWith),
        //                                [exp.Type.GetGenericArguments()[0]],
        //                                exp,
        //                                Expression.Constant($"GQL op: {op ?? "n/a"}, field: {field}")
        //                            );
        //                        return exp;
        //                    },
        //#if DEBUG
        //                    // IncludeDebugInfo = true
        //#endif
        //                }
        //            );
        //        });

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
