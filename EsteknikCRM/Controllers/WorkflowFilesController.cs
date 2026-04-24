using EsteknikCRM.Api.Data;
using EsteknikCRM.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkflowFilesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public WorkflowFilesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("upload/{workflowId}")]
        [RequestSizeLimit(50_000_000)]
        public async Task<IActionResult> Upload(string workflowId, [FromForm] List<IFormFile> files)
        {
            try
            {
                var workflowExists = await _context.Workflows.AnyAsync(x => x.Id == workflowId);
                if (!workflowExists)
                    return NotFound("İş akışı bulunamadı.");

                if (files == null || files.Count == 0)
                    return BadRequest("Dosya seçilmedi.");

                var uploadRoot = Path.Combine(_env.ContentRootPath, "uploads", "workflows", workflowId);
                Directory.CreateDirectory(uploadRoot);

                var savedFiles = new List<WorkflowFile>();

                foreach (var file in files)
                {
                    if (file.Length <= 0)
                        continue;

                    var storedFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var fullPath = Path.Combine(uploadRoot, storedFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var entity = new WorkflowFile
                    {
                        Id = Guid.NewGuid().ToString(),
                        WorkflowId = workflowId,
                        FileName = file.FileName,
                        StoredFileName = storedFileName,
                        FilePath = fullPath,
                        ContentType = file.ContentType,
                        FileSize = file.Length,
                        CreatedDate = DateTime.UtcNow
                    };

                    _context.WorkflowFiles.Add(entity);
                    savedFiles.Add(entity);
                }

                await _context.SaveChangesAsync();
                return Ok(savedFiles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{workflowId}")]
        public async Task<IActionResult> GetByWorkflow(string workflowId)
        {
            var files = await _context.WorkflowFiles
                .Where(x => x.WorkflowId == workflowId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(files);
        }
    }
}