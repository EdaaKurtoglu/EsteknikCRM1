using EsteknikCRM.Api.Data;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: api/teams
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);
        }

        // 🔹 GET: api/teams/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
                return NotFound("Takım bulunamadı");

            return Ok(team);
        }

        // 🔹 POST: api/teams
        [HttpPost]
        public async Task<IActionResult> Add(Team team)
        {
            team.CreatedDate = DateTime.UtcNow;

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return Ok(team);
        }

        // 🔹 PUT: api/teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Team updatedTeam)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
                return NotFound("Takım bulunamadı");

            team.TeamId = updatedTeam.TeamId;
            team.ServiceName = updatedTeam.ServiceName;
            team.TeamName = updatedTeam.TeamName;
            team.MemberCount = updatedTeam.MemberCount;
            team.MemberName = updatedTeam.MemberName;
            team.Role = updatedTeam.Role;
            team.Status = updatedTeam.Status;
            team.VehiclePlate = updatedTeam.VehiclePlate;
            team.IsActive = updatedTeam.IsActive;
            team.PassiveDate = updatedTeam.PassiveDate;

            await _context.SaveChangesAsync();

            return Ok(team);
        }

        // 🔹 DELETE: api/teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
                return NotFound("Takım bulunamadı");

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }
    }
}