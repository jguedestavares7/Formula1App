using Formula1API.Models;

namespace Formula1API.Dtos
{
    public class RaceDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int NumberLaps { get; set; }
        public string Date { get; set; }

        public int? WinnerDriverId { get; set; }
    }
}
