using Formula1API.Data;
using Formula1API.Dtos;
using Formula1API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Formula1API.Services;

namespace Formula1API.ServicesImpl
{
    public class RaceService : Exception, IRaceService
    {
        private readonly Formula1DbContext _dbContext;
        private readonly IMapper _mapper;

        public RaceService(Formula1DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<Race>> GetAllRaces()
        {
            var races = await _dbContext.Races
                .Include(r => r.WinnerDriver)
                .ToListAsync();

            return races.Select(r => _mapper.Map<Race>(r)).ToList();
        }

        public async Task<Race> GetRace(int id)
        {
            var race = await _dbContext.Races
                .Include(r => r.WinnerDriver)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (race == null)
            {
                throw new Exception("Race not found");
            }

            return _mapper.Map<Race>(race);
        }

        public async Task<Race> AddRace(RaceDto raceDto)
        {
            var race = new Race
            {
                Name = raceDto.Name,
                Country = raceDto.Country,
                NumberLaps = raceDto.NumberLaps,
                Date = DateTime.ParseExact(raceDto.Date, "yyyyMMddHHmmss", null),
                WinnerDriverId = null,
                WinnerDriver = null
            };

            if (raceDto.WinnerDriverId.HasValue)
            {
                var driverExists = await _dbContext.Drivers.AnyAsync(d => d.Id == raceDto.WinnerDriverId);
                if (!driverExists)
                {
                    throw new Exception($"Driver with ID {raceDto.WinnerDriverId} does not exist.");
                }

                var winnerDriver = await _dbContext.Drivers.FindAsync(raceDto.WinnerDriverId);

                race.WinnerDriver = winnerDriver;
                race.WinnerDriverId = winnerDriver.Id;
            }
            _dbContext.Races.Add(race);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Race>(race);
        }

        public async Task<Race> EditRace(int id, RaceDto raceDto)
        {
            var race = await _dbContext.Races
                .Include(r => r.WinnerDriver)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (race == null)
            {
                throw new Exception("Race not found");
            }

            race.Name = raceDto.Name;
            race.Country = raceDto.Country;
            race.NumberLaps = raceDto.NumberLaps;
            race.Date = DateTime.ParseExact(raceDto.Date, "yyyyMMddHHmmss", null);

            var winnerDriver = await _dbContext.Drivers.FirstOrDefaultAsync(d => d.Id == raceDto.WinnerDriverId);

            if (winnerDriver != null)
            {
                race.WinnerDriverId = raceDto.WinnerDriverId;
                race.WinnerDriver = winnerDriver;
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Race>(race);
        }

        public async Task DeleteRace(int id)
        {
            var race = await _dbContext.Races.FindAsync(id);

            if (race == null)
            {
                throw new Exception("Race not found");
            }

            _dbContext.Races.Remove(race);
            await _dbContext.SaveChangesAsync();

        }


    }
}
