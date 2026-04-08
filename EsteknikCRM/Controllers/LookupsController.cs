using EsteknikCRM.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LookupsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories
                .Select(x => new
                {
                    ID = x.Id,
                    Category = x.CategoryName
                })
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("notificationtypes")]
        public async Task<IActionResult> GetNotificationTypes()
        {
            var notificationTypes = await _context.NotificationTypes
                .Select(x => new
                {
                    ID = x.Id,
                    NotificationType = x.NotifyType
                })
                .ToListAsync();

            return Ok(notificationTypes);
        }

        [HttpGet("subcategories")]
        public async Task<IActionResult> GetSubCategories()
        {
            var subCategories = await _context.SubCategories
                .Select(x => new
                {
                    ID = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return Ok(subCategories);
        }
    }
}