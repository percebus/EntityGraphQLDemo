namespace JCystems.GettingStarted.Connectors.Data
{
    using System.Linq;
    using JCystems.GettingStarted.Connectors.Contexts;
    using JCystems.GettingStarted.Connectors.Entities;

    // SRC: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-9.0
    public static class DbInitializer
    {
        public static void Initialize(DemoContext context)
        {
            context.Database.EnsureCreated();

            if (context.People.Any())
            {
                return; // DB has been seeded
            }

            var people = new Person[]
            {
            };

            foreach (Person person in people)
            {
                context.People.Add(person);
            }

            context.SaveChanges();
        }
    }
}
