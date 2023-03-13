using AutoMapper;
using Formula1API.Data;
using Formula1API.Dtos;
using Formula1API.Models;
using Formula1API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace Formula1API.ServicesImpl
{
    public class DriverService : Exception, IDriverService
    {
        private readonly Formula1DbContext _dbContext;
        private readonly IMapper _mapper;

        public DriverService(Formula1DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<Driver>> GetAllDrivers()
        {
            var driver = await _dbContext.Drivers
                .Include(d => d.Races)
                .Include(d => d.Team)
                .ToListAsync();

            return driver.Select(d => _mapper.Map<Driver>(d)).ToList();
        }

        public async Task<Driver> GetDriver(int id)
        {
            var driver = await _dbContext.Drivers
                .Include(d => d.Races)
                .Include(d => d.Team)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                throw new Exception("Driver not found");
            }

            return _mapper.Map<Driver>(driver);
        }

        public async Task<ICollection<Race>> GetRacesOfDriver(int id)
        {
            var driver = await _dbContext.Drivers
                .Include(d => d.Races)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (driver == null)
            {
                throw new Exception("Driver not found");
            }
            return _mapper.Map<ICollection<Race>>(driver.Races);

        }

        public async Task<Driver> AddDriver(DriverDto driverDto)
        {
            var team = await _dbContext.Teams.FindAsync(driverDto.TeamId);

            var driver = new Driver
            {
                Name = driverDto.Name,
                Number = driverDto.Number,
                Abbreviation = driverDto.Abbreviation,
                Nationality = driverDto.Nationality,
                Birthday = DateTime.ParseExact(driverDto.Birthday, "yyyyMMddHHmmss", null),
                Team = team
            };

            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Driver>(driver);
        }

        public async Task<Driver> EditDriver(int id, DriverDto driverDto)
        {
            var driver = await _dbContext.Drivers.FindAsync(id);

            if (driver == null)
            {
                throw new Exception("Driver not found");
            }

            driver.Name = driverDto.Name;
            driver.Number = driverDto.Number;
            driver.Abbreviation = driverDto.Abbreviation;
            driver.Nationality = driverDto.Nationality;
            driver.Birthday = DateTime.ParseExact(driverDto.Birthday, "yyyyMMddHHmmss", null);

            var team = await _dbContext.Teams.FirstOrDefaultAsync(t => t.Id == driverDto.TeamId);

            driver.Team = team;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Driver>(driver);
        }

        public async Task DeleteDriver(int id)
        {
            var driver = await _dbContext.Drivers.FindAsync(id);

            if (driver == null)
            {
                throw new Exception("Driver not found");
            }

            _dbContext.Drivers.Remove(driver);
            await _dbContext.SaveChangesAsync();
        }
    }
}
