using Formula1API.Data;
using Formula1API.Models;
using Microsoft.EntityFrameworkCore;

namespace Formula1API.TestsFixture
{
    public class InMemoryDatabaseFixture : IDisposable
    {

        public InMemoryDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<Formula1DbContext>()
                .UseInMemoryDatabase(databaseName: "TestsDatabase")
                .Options;

            _context = new Formula1DbContext(options);

            var drivers = new List<Driver>
            {
                new Driver { Id = 1, Name = "Driver 1", Number = 10, Abbreviation = "D1", Nationality = "PT", Birthday = new DateTime(2000, 01, 01) },
                new Driver { Id = 2, Name = "Driver 2", Number = 20, Abbreviation = "D2", Nationality = "PT", Birthday = new DateTime(2000, 01, 02) },
                new Driver { Id = 3, Name = "Driver 3", Number = 30, Abbreviation = "D3", Nationality = "PT", Birthday = new DateTime(2000, 01, 03) }
            };

            _context.Drivers.AddRange(drivers);
            _context.SaveChanges();

            var races = new List<Race>
            {
                new Race { Id = 1, Name = "Race 1", Country = "PT", NumberLaps = 50, Date = new DateTime(2023, 3, 5) },
                new Race { Id = 2, Name = "Race 3", Country = "PT", NumberLaps = 50, Date = new DateTime(2023, 3, 5) },
                new Race { Id = 3, Name = "Race 3", Country = "PT", NumberLaps = 50, Date = new DateTime(2023, 3, 5) },
            };
            _context.Races.AddRange(races);
            _context.SaveChanges();

            var teams = new List<Team>
            {
                new Team { Id = 1, Name = "Team 1", CarName = "Car 1", Engine = "Engine 1", Director = "Director 1" },
                new Team { Id = 2, Name = "Team 2", CarName = "Car 2", Engine = "Engine 2", Director = "Director 2" },
                new Team { Id = 3, Name = "Team 3", CarName = "Car 3", Engine = "Engine 3", Director = "Director 3" },
            };
            _context.Teams.AddRange(teams);
            _context.SaveChanges();
        }

        public Formula1DbContext _context { get; }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
