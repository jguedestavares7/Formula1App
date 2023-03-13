namespace Formula1API.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Abbreviation { get; set; }
        public string Nationality { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Race> Races { get; set; }
        public int? TeamId { get; set; }
        public Team? Team { get; set; }

    }
}
