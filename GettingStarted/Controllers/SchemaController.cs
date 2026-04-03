namespace JCystems.GettingStarted.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("graphql/schema")]
    public class SchemaController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            string sdl = await System.IO.File.ReadAllTextAsync("AppData/schema.graphql");
            return sdl;
        }
    }
}
