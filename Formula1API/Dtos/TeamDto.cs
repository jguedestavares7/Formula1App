using Formula1API.Models;

namespace Formula1API.Dtos
{
    public class TeamDto
    {
        public string Name { get; set; }
        public string CarName { get; set; }
        public string Engine { get; set; }
        public string Director { get; set; }
        public int[] DriversIds { get; set; }
    }
}
