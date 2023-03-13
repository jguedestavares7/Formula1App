using Microsoft.AspNetCore.Mvc;
using Formula1API.Models;
using Formula1API.Dtos;

namespace Formula1API.Services
{
    public interface IRaceService
    {
        Task<ICollection<Race>> GetAllRaces();
        Task<Race> GetRace(int id);
        Task<Race> AddRace(RaceDto raceDto);
        Task<Race> EditRace(int id, RaceDto raceDto);
        Task DeleteRace(int id);
    }
}
