namespace JCystems.GettingStarted.Connectors.Contexts
{
    using JCystems.GettingStarted.Connectors.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DemoContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Writer> Writers { get; set; }

        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }
    }
}
