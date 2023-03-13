namespace Formula1API.Models
{
    public class Race
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int NumberLaps { get; set; }
        public DateTime Date { get; set; }

        public int? WinnerDriverId { get; set; }
        public Driver? WinnerDriver { get; set; }

    }
}
