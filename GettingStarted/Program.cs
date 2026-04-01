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

        builder
            .AddGraphQL()
            .AddTypes(Array.Empty<Type>());

        builder.Services
            .AddDbContext<DemoContext>(options => options.UseSqlServer(config.Database.ReadConnectionString))
            .AddGraphQLSchema<DemoContext>();

        var app = builder.Build();
        CreateDbIfNotExists(app.Services);

        app.MapGraphQL<DemoContext>(followSpec: true);

        app
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

        app.RunWithGraphQLCommands(args);
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