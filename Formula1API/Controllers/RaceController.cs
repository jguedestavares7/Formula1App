using Formula1API.Data;
using Formula1API.Dtos;
using Formula1API.Models;
using Formula1API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Formula1API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceController : Controller
    {
        private readonly IRaceService _raceService;
        public RaceController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Race>>> GetAllRaces()
        {
            var races = await _raceService.GetAllRaces();
            return Ok(races);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Race>> GetRace(int id)
        {
            var race = await _raceService.GetRace(id);

            return Ok(race);
        }

        [HttpPost]
        public async Task<ActionResult<Race>> AddRace([FromBody] RaceDto raceDto)
        {
            var race = await _raceService.AddRace(raceDto);

            return Ok(race);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Race>> EditRace(int id, [FromBody] RaceDto raceDto)
        {
            var race = await _raceService.EditRace(id, raceDto);

            return Ok(race);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRace(int id)
        {
            await _raceService.DeleteRace(id);

            return Ok(true);
        }
    }
}
