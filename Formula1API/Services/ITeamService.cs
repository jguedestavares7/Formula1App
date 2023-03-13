using Formula1API.Dtos;
using Formula1API.Models;

namespace Formula1API.Services
{
    public interface ITeamService
    {
        Task<ICollection<Team>> GetAllTeams();
        
        Task<Team> GetTeam(int id);
        
        Task<Team> GetTeamOfDriver(int id);
        
        Task<ICollection<Driver>> GetDriversOfTeam(int id);
        
        Task<ICollection<Race>> GetRacesOfTeam(int id);
        
        Task<Team> AddTeam(TeamDto teamDto);
        
        Task<Team> EditTeam(int id, TeamDto teamDto);

        Task DeleteTeam(int id);
    }
}
