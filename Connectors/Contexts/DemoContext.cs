namespace EntityGraphQLDemo.Connectors.Contexts
{
    using EntityGraphQLDemo.Connectors.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DemoContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Writer> Writers { get; set; }
    }
}
