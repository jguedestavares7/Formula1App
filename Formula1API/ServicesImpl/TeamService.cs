using AutoMapper;
using Formula1API.Data;
using Formula1API.Dtos;
using Formula1API.Models;
using Formula1API.Services;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Formula1API.ServicesImpl
{
    public class TeamService : Exception, ITeamService
    {
        private readonly Formula1DbContext _dbContext;
        private readonly IMapper _mapper;

        public TeamService(Formula1DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<Team>> GetAllTeams()
        {
            var team = (await _dbContext.Teams
                .Include(t => t.Drivers)
                .ToListAsync());

            return team.Select(t => _mapper.Map<Team>(t)).ToList();
        }

        public async Task<Team> GetTeam(int id)
        {
            var team = await _dbContext.Teams
                .Include(t => t.Drivers)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                throw new Exception("Team not found");
            }

            return _mapper.Map<Team>(team);
        }

        public async Task<Team> GetTeamOfDriver(int id)
        {
            var driver = await _dbContext.Drivers.FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                throw new Exception("Driver not found");
            }

            var team = await _dbContext.Teams
                .FirstOrDefaultAsync(t => t.Drivers.Any(d => d.Id == driver.Id));

            if (team == null)
            {
                throw new Exception("Team not found");
            }

            return _mapper.Map<Team>(team);
        }

        public async Task<ICollection<Driver>> GetDriversOfTeam(int id)
        {
            var team = await _dbContext.Teams.Include(t => t.Drivers).FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                throw new Exception("Team not found");
            }

            return _mapper.Map<ICollection<Driver>>(team.Drivers);
        }

        public async Task<ICollection<Race>> GetRacesOfTeam(int id)
        {
            var team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                throw new Exception("Team not found");
            }

            var races = await _dbContext.Races
                .Include(r => r.WinnerDriver)
                .ThenInclude(d => d.Team)
                .Where(r => r.WinnerDriver.Team.Id == id)
                .ToListAsync();

            return _mapper.Map<ICollection<Race>>(races);
        }

        public async Task<Team> AddTeam(TeamDto teamDto)
        {
            var team = new Team
            {
                Name = teamDto.Name,
                CarName = teamDto.CarName,
                Engine = teamDto.Engine,
                Director = teamDto.Director,
            };

            var drivers = await _dbContext.Drivers.Where(d => teamDto.DriversIds.Contains(d.Id)).ToListAsync();

            team.Drivers.AddRange(drivers);

            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Team>(team);
        }

        public async Task<Team> EditTeam(int id, TeamDto teamDto)
        {
            var team = await _dbContext.Teams.FindAsync(id);

            if (team == null)
            {
                throw new Exception("Team not found");
            }

            team.Name = teamDto.Name;
            team.CarName = teamDto.CarName;
            team.Engine = teamDto.Engine;
            team.Director = teamDto.Director;

            var drivers = await _dbContext.Drivers.Where(d => teamDto.DriversIds.Contains(d.Id)).ToListAsync();

            team.Drivers.AddRange(drivers);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Team>(team);
        }

        public async Task DeleteTeam(int id)
        {
            var team = await _dbContext.Teams.FindAsync(id);

            if (team == null)
            {
                throw new Exception("Team not found");
            }

            _dbContext.Teams.Remove(team);
            await _dbContext.SaveChangesAsync();
        }
    }
}
