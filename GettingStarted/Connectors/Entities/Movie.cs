namespace JCystems.GettingStarted.Connectors.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public uint Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public DateTime Released { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Writer> Writers { get; set; }
        public Person Director { get; set; }
        public uint? DirectorId { get; set; }
        public double Rating { get; internal set; }
    }
}
