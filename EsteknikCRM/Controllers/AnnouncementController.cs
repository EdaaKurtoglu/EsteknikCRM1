using EsteknikCRM.Api.Data;
using EsteknikCRM.Api.Entities;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : Controller
    {
        private readonly AppDbContext _context;
        public AnnouncementController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var annoucement = await _context.Announcements.ToListAsync();
            return Ok(annoucement);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var annoucement = await _context.Announcements.FindAsync(id);

            if (annoucement == null)
                return NotFound();

            return Ok(annoucement);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Announcement annoucement)
        {
            _context.Announcements.Add(annoucement);
            await _context.SaveChangesAsync();

            return Ok(annoucement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Announcement updatedAnnouncement)
        {
            var annoucement = await _context.Announcements.FindAsync(id);

            if (annoucement == null)
                return NotFound();

            annoucement.Subject = updatedAnnouncement.Subject;
            annoucement.DateText = updatedAnnouncement.DateText;
            annoucement.BodyText = updatedAnnouncement.BodyText;
            annoucement.AttachmentFileName = updatedAnnouncement.AttachmentFileName;
            annoucement.AttachmentSizeText = updatedAnnouncement.AttachmentSizeText;
            annoucement.GroupText = updatedAnnouncement.GroupText;
            annoucement.CreatedDate = updatedAnnouncement.CreatedDate;
            annoucement.FileNames = updatedAnnouncement.FileNames;
            annoucement.FileUrls = updatedAnnouncement.FileUrls;
            annoucement.FileSizes = updatedAnnouncement.FileSizes;
            


            await _context.SaveChangesAsync();

            return Ok(annoucement);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);

            if (announcement == null)
                return NotFound();

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }
    }
}
