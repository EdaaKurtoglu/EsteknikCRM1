using EsteknikCRM.Api.Data;
using EsteknikCRM.Api.Entities;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnnouncementController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcements = await _context.Announcements
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            var result = announcements.Select(x => new
            {
                x.Id,
                x.Subject,
                x.BodyText,
                x.CreatedDate,
                DateText = x.CreatedDate.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),

                GroupText = GetGroupText(x.CreatedDate.ToLocalTime())
            }).ToList();

            return Ok(result);
        }

        private string GetGroupText(DateTime date)
        {
            int days = (DateTime.Now.Date - date.Date).Days;

            if (days <= 0) return "Bugün";
            if (days == 1) return "1 gün önce";
            return $"{days} gün önce";
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

        [HttpGet("download/{fileId}")]
        public async Task<IActionResult> Download(string fileId)
        {
            var file = await _context.AnnouncementFiles.FindAsync(fileId);

            if (file == null)
                return NotFound("Dosya bulunamadı.");

            if (!System.IO.File.Exists(file.FilePath))
                return NotFound("Fiziksel dosya bulunamadı.");

            var bytes = await System.IO.File.ReadAllBytesAsync(file.FilePath);

            return File(bytes, file.ContentType ?? "application/octet-stream", file.FileName);
        }
        [HttpGet("files/{announcementId}")]
        public async Task<IActionResult> GetFiles(string announcementId)
        {
            var files = await _context.AnnouncementFiles
                .Where(x => x.AnnouncementId == announcementId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(files);
        }
        [HttpPost("upload/{announcementId}")]
        public async Task<IActionResult> Upload(string announcementId, [FromForm] List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return BadRequest("Dosya seçilmedi.");

                var uploadFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "uploads",
                    "announcements",
                    announcementId);

                Directory.CreateDirectory(uploadFolder);

                foreach (var file in files)
                {
                    if (file.Length <= 0)
                        continue;

                    var storedFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var fullPath = Path.Combine(uploadFolder, storedFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    _context.AnnouncementFiles.Add(new AnnouncementFile
                    {
                        Id = Guid.NewGuid().ToString(),
                        AnnouncementId = announcementId,
                        FileName = file.FileName,
                        StoredFileName = storedFileName,
                        FilePath = fullPath,
                        ContentType = file.ContentType,
                        FileSize = file.Length,
                        CreatedDate = DateTime.UtcNow
                    });
                }

                await _context.SaveChangesAsync();

                return Ok("Dosyalar yüklendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}