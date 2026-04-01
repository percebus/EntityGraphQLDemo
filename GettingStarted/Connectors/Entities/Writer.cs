namespace JCystems.GettingStarted.Connectors.Entities
{
    public class Writer
    {
        public int Id { get; set; }
        public uint PersonId { get; set; }
        public Person Person { get; set; }
        public uint MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
