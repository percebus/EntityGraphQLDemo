namespace JCystems.GettingStarted.Controllers
{
    using EntityGraphQL.Schema;
    using JCystems.GettingStarted.Connectors.Contexts;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("graphql/schema")]
    public class SchemaController : ControllerBase
    {
        [HttpGet]
        public string Get(SchemaProvider<DemoContext> schema)
        {
            //string sdl = await System.IO.File.ReadAllTextAsync("AppData/schema.graphql");
            //return sdl;

            return schema.ToGraphQLSchemaString();
        }
    }
}
