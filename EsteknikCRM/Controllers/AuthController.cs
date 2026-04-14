using EsteknikCRM.Api.Data;
using EsteknikCRM.Api.Entities;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest("Geçersiz istek");

            var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.UserMail == request.UserMail &&
                x.Password == request.Password &&
                x.UserRole == request.UserRole);

            if (user == null)
                return Unauthorized("Email veya şifre hatalı");

            return Ok(user);
        }
        [HttpPost("users")]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                user.Id = Guid.NewGuid().ToString();
                user.UserMail ??= "";
                user.Password ??= "";
                user.UserRole ??= "";
                user.Name ??= "";
                user.Surname ??= "";
                user.MiddleName ??= "";
                user.Department ??= "";

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
        public class LoginRequest
        {
            public string UserMail { get; set; }
            public string Password { get; set; }
            public string UserRole { get; set; }
        }
    }
}