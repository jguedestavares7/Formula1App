using Formula1API.Data;
using Formula1API.Dtos;
using Formula1API.Models;
using Formula1API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Formula1API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Driver>>> GetAllDrivers()
        {
            var drivers = await _driverService.GetAllDrivers();
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var driver = await _driverService.GetDriver(id);

            return Ok(driver);
        }

        [HttpGet("{id}/races")]
        public async Task<ActionResult<ICollection<Race>>> GetRacesOfDriver(int id)
        {
            var races = await _driverService.GetRacesOfDriver(id);

            return Ok(races);
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> AddDriver([FromBody] DriverDto driverDto)
        {
            var driver = await _driverService.AddDriver(driverDto);

            return Ok(driver);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Driver>> EditDriver(int id, [FromBody] DriverDto driverDto)
        {
            var driver = await _driverService.EditDriver(id, driverDto);

            return Ok(driver);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDriver(int id)
        {
            await _driverService.DeleteDriver(id);

            return Ok(true);
        }
    }
}
