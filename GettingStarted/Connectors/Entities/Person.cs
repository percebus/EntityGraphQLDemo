namespace JCystems.GettingStarted.Connectors.Entities
{
    public class Person
    {
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public List<Actor> ActorIn { get; set; }
        public List<Writer> WriterOf { get; set; }
        public List<Movie> DirectorOf { get; set; }
        public DateTime? Died { get; set; }
        public bool IsDeleted { get; set; }
    }
}
