namespace JCystems.GettingStarted.Options
{
    public class ServiceOptions
    {
        public static string Key = "EntityGraphQLDemo";

        public DatabaseOptions Database { get; set; } = new();
    }
}
