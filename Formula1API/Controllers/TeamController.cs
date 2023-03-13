using Formula1API.Data;
using Formula1API.Dtos;
using Formula1API.Models;
using Formula1API.Services;
using Formula1API.ServicesImpl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Formula1API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Team>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeams();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _teamService.GetTeam(id);

            return Ok(team);
        }

        [HttpGet("driver/{id}")]
        public async Task<ActionResult<Team>> GetTeamOfDriver(int id)
        {
            var team = await _teamService.GetTeamOfDriver(id);

            return Ok(team);
        }

        [HttpGet("{id}/drivers")]
        public async Task<ActionResult<ICollection<Driver>>> GetDriversOfTeam(int id)
        {
            var drivers = await _teamService.GetDriversOfTeam(id);

            return Ok(drivers);
        }

        [HttpGet("{id}/races")]
        public async Task<ActionResult<ICollection<Race>>> GetRacesOfTeam(int id)
        {
            var races = await _teamService.GetRacesOfTeam(id);

            return Ok(races);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> AddTeam([FromBody] TeamDto teamDto)
        {
            var team = await _teamService.AddTeam(teamDto);

            return Ok(team);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Team>> EditTeam(int id, [FromBody] TeamDto teamDto)
        {
            var team = await _teamService.EditTeam(id, teamDto);

            return Ok(team);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            await _teamService.DeleteTeam(id);

            return Ok(true);
        }
    }
}
