using AutoMapper;
using Formula1API.Controllers;
using Formula1API.Data;
using Formula1API.Dtos;
using Formula1API.Models;
using Formula1API.Profiles;
using Formula1API.Services;
using Formula1API.ServicesImpl;
using Formula1API.TestsFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Formula1API.Tests
{
    [Collection("Database collection")]
    public class RaceControllerTests
    {
        private readonly IRaceService _raceService;
        private readonly RaceController _controller;
        private readonly Formula1DbContext _dbContext;
        private readonly IMapper _mapper;

        public RaceControllerTests(InMemoryDatabaseFixture fixture)
        {
            _dbContext = fixture._context;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SourceMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _raceService = new RaceService(_dbContext, _mapper);
            _controller = new RaceController(_raceService);
        }

        [Fact]
        public async Task Test_AddRace()
        {
            //Arrange
            var race = new RaceDto { Name = "New Race", Country = "PT", NumberLaps = 35, Date = "20201010000000" };

            //Act
            var result = await _controller.AddRace(race);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualRace = Assert.IsAssignableFrom<Race>(okResult.Value);

            Assert.Equal("New Race", actualRace.Name);
        }

        [Fact]
        public async Task Test_DeleteRace()
        {
            //Arrange

            //Act
            var result = await _controller.DeleteRace(3);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultBool = Assert.IsType<bool>(okResult.Value);

            Assert.True(resultBool);

        }

        [Fact]
        public async Task Test_EditRace()
        {
            //Arrange

            //Act
            var race2 = new RaceDto { Name = "Race edited 2", Country = "PT", NumberLaps = 35, Date = "20201010000000" };
            var result = await _controller.EditRace(2, race2);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualRace = Assert.IsAssignableFrom<Race>(okResult.Value);

            Assert.Equal("Race edited 2", actualRace.Name);
        }

        [Fact]
        public async Task Test_GetRace()
        {

            //Arrange

            //Act
            var result = await _controller.GetRace(1);

            //Assert

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualRaces = Assert.IsAssignableFrom<Race>(okResult.Value);

            Assert.Equal("Race 1", actualRaces.Name);
        }
    }
}
