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
    public class TeamControllerTests
    {
        private readonly ITeamService _teamService;
        private readonly TeamController _controller;
        private readonly Formula1DbContext _dbContext;
        private readonly IMapper _mapper;

        public TeamControllerTests(InMemoryDatabaseFixture fixture)
        {
            _dbContext = fixture._context;
            
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SourceMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _teamService = new TeamService(_dbContext, _mapper);
            _controller = new TeamController(_teamService);

        }

        [Fact]
        public async Task Test_AddTeam()
        {
            //Arrange
            var team = new TeamDto { Name = "New Team", CarName = "Team 2 car", Engine = "Team 2 engine", Director = "Team 2 director", DriversIds = new int[] { } };
            
            //Act
            var result = await _controller.AddTeam(team);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualTeam = Assert.IsAssignableFrom<Team>(okResult.Value);

            Assert.Equal("New Team", actualTeam.Name);
        }
        

        [Fact]
        public async Task Test_DeleteTeam()
        {
            //Arrange

            //Act
            var result = await _controller.DeleteTeam(3);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultBool = Assert.IsType<bool>(okResult.Value);

            Assert.True(resultBool);
        }

        [Fact]
        public async Task Test_EditTeam()
        {
            //Arrange

            //Act
            var team2 = new TeamDto { Name = "Mercedes", CarName = "Mercedes-AMG 2023", Engine = "Mercedes", Director = "Toto Wolff", DriversIds = new int[] { } };
            var result = await _controller.EditTeam(2, team2);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualTeam = Assert.IsAssignableFrom<Team>(okResult.Value);

            Assert.Equal("Mercedes-AMG 2023", actualTeam.CarName);
        }

        [Fact]
        public async Task Test_GetTeam()
        {
            //Arrange

            //Act
            var result = await _controller.GetTeam(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualTeam = Assert.IsAssignableFrom<Team>(okResult.Value);

            Assert.Equal("Team 1", actualTeam.Name);
        }

        
    }
}
