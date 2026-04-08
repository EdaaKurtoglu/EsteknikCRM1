using EsteknikCRM.Api.Data;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnnouncementsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcements = await _context.Announcements
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new
                {
                    x.Id,
                    x.Subject,
                    x.BodyText,
                    x.CreatedDate,
                    DateText = x.CreatedDate.ToString("dd/MM/yyyy HH:mm"),
                    GroupText = x.CreatedDate.Date == DateTime.Now.Date
                        ? "Bugün"
                        : (DateTime.Now.Date - x.CreatedDate.Date).Days == 1
                            ? "1 gün önce"
                            : (DateTime.Now.Date - x.CreatedDate.Date).Days + " gün önce",
                    x.FileNames,
                    x.FileUrls,
                    x.FileSizes
                })
                .ToListAsync();

            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var announcement = await _context.Announcements.FindAsync(id);

            if (announcement == null)
                return NotFound("Duyuru bulunamadı");

            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Announcement model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.Subject ??= "";
                model.BodyText ??= "";
                model.CreatedDate = model.CreatedDate == default ? DateTime.UtcNow : model.CreatedDate;

                _context.Announcements.Add(model);
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var announcement = await _context.Announcements.FindAsync(id);

            if (announcement == null)
                return NotFound("Duyuru bulunamadı");

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }
    }
}