using EsteknikCRM.Api.Data;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HakedisSetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HakedisSetsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.HakedisSets
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HakedisSet model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.ServiceTitle ??= "";
                model.SapServiceCode ??= "";
                model.ServiceResponsible ??= "";
                model.InvoiceNumber ??= "";
                model.InvoiceDate ??= "";
                model.PreApprovalDate ??= "";
                model.PreApprovalApproveDate ??= "";
                model.SetDate ??= "";
                model.SetApproveDate ??= "";
                model.ExportDate ??= "";
                model.PayType ??= "";
                model.CreatedDate = model.CreatedDate == default ? DateTime.UtcNow : model.CreatedDate;

                _context.HakedisSets.Add(model);
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}