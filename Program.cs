using EntityGraphQL.AspNet;
using EntityGraphQLDemo.Connectors.Contexts;

var builder = WebApplication.CreateSlimBuilder(args);


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder
    .Services
        .AddOpenApi()
        .AddDbContext<DemoContext>(opt => opt.UseInMemoryDatabase("Demo"))
        .AddGraphQLSchema<DemoContext>();

var app = builder.Build();

app.MapGraphQL<DemoContext>();
app
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();
