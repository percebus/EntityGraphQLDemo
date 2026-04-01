using System.Text.Json;
using System.Text.Json.Serialization;
using EntityGraphQL.AspNet;
using JCystems.GettingStarted.Connectors.Contexts;
using JCystems.GettingStarted.Connectors.Data;
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
            .AddDbContext<DemoContext>(options => options.UseSqlServer(config.Database.ReadConnectionString))
            .AddGraphQLSchema<DemoContext>()
        //.AddControllers()
        //.AddJsonOptions(opts =>
        //{
        //    var converter = new JsonStringEnumConverter();
        //    // Use enum field names instead of numbers
        //    opts.JsonSerializerOptions.Converters.Add(converter);
        //    // EntityGraphQL internally builds types with fields
        //    opts.JsonSerializerOptions.IncludeFields = true;
        //    // The fields internally built already are named with fieldNamer (defaults to camelCase). This is
        //    // for the properties on QueryResult (Data, Errors) to match what most tools etc expect (camelCase)
        //    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        //})
        ;

        var app = builder.Build();
        CreateDbIfNotExists(app.Services);


        app.UseRouting();

        // app.MapGraphQL<DemoContext>(followSpec: true);
        app.UseEndpoints(endpoints =>
        {
            // defaults to /graphql endpoint
            endpoints.MapGraphQL<DemoContext>(configureEndpoint: (endpoint) =>
            {
                // endpoint.RequireAuthorization("authorized");
                // do other things with endpoint
            });
        });

        app.Run();
    }

    private static void CreateDbIfNotExists(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            try
            {
                var context = scopedServices.GetRequiredService<DemoContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = scopedServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}