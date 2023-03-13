namespace Formula1API.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CarName { get; set; }
        public string Engine { get; set; }
        public string Director { get; set; }

        public ICollection<Driver> Drivers { get; set; }
    }
}
