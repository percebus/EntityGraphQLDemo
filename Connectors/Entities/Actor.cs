namespace EntityGraphQLDemo.Connectors.Entities
{
    public class Actor
    {
        public uint PersonId { get; set; }
        public Person Person { get; set; }
        public uint MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
