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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Formula1API.Tests
{
    [Collection("Database collection")]
    public class DriverControllerTests
    {
        private readonly IDriverService _driverService;
        private readonly DriverController _controller;
        private readonly Formula1DbContext _dbContext;
        private readonly IMapper _mapper;

        public DriverControllerTests(InMemoryDatabaseFixture fixture)
        {
            _dbContext = fixture._context;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SourceMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _driverService = new DriverService(_dbContext, _mapper);
            _controller = new DriverController(_driverService);
        }
        
        [Fact]
        public async Task Test_AddDriver()
        {
            //Arrange
            var driver = new DriverDto { Name = "New Driver", Number = 11, Abbreviation = "ND1", Nationality = "Nation 1", Birthday = "20001012000000" };
            //Act
            var result = await _controller.AddDriver(driver);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualDriver = Assert.IsAssignableFrom<Driver>(okResult.Value);

            Assert.Equal("New Driver", actualDriver.Name);

        }

        [Fact]
        public async Task Test_DeleteDriver()
        {
            //Arrange

            //Act
            var result = await _controller.DeleteDriver(3);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultBool = Assert.IsType<bool>(okResult.Value);

            Assert.True(resultBool);
        }
        
        [Fact]
        public async Task Test_EditDriver()
        {
            //Arrange

            //Act
            var editDriver = new DriverDto { Name = "Name", Number = 1, Abbreviation = "D1", Nationality = "Nation 1", Birthday = "20000101000000" };
            var result = await _controller.EditDriver(2, editDriver);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualDriver = Assert.IsAssignableFrom<Driver>(okResult.Value);

            Assert.Equal("Name", actualDriver.Name);
        }

        [Fact]
        public async Task Test_GetDriver()
        {
            //Arrange

            //Act
            var result = await _controller.GetDriver(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualDriver = Assert.IsAssignableFrom<Driver>(okResult.Value);

            Assert.Equal("Driver 1", actualDriver.Name);

        }
    }
}
