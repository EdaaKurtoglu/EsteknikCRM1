using EsteknikCRM.Api.Data;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OperationsController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 fiyat çekme
        [HttpGet("price")]
        public async Task<IActionResult> GetPrice([FromQuery] string stockCode, [FromQuery] string operationType)
        {
            var priceRow = await _context.OperationPrices
                .FirstOrDefaultAsync(x =>
                    x.OperationType == operationType);

            if (priceRow == null)
                return Ok(0);

            return Ok(priceRow.Price);
        }

        // 🔹 yeni fiyat ekleme
        [HttpPost]
        public async Task<IActionResult> Add(OperationPrice model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.StockCode ??= "";
                model.OperationType ??= "";
                model.Price = model.Price;

                _context.OperationPrices.Add(model);
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        // 🔹 listeleme (opsiyonel)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.OperationPrices.ToListAsync();
            return Ok(list);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetOperationTypes()
        {
            var types = await _context.OperationPrices
                .Select(x => x.OperationType)
                .Where(x => x != null && x != "")
                .Distinct()
                .ToListAsync();

            return Ok(types);
        }
        [HttpPost("workflow-team-operation")]
        public async Task<IActionResult> AddWorkflowTeamOperation(WorkflowTeamOperation model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.WorkflowId ??= "";
                model.CustomerId ??= "";
                model.DeviceId ??= "";
                model.SerialNumber ??= "";
                model.StockCode ??= "";
                model.DeviceName ??= "";
                model.OperationType ??= "";
                model.CreatedByUserMail ??= "";
                model.CreatedByName ??= "";
                model.CreatedBySurname ??= "";
                model.CreatedByRole ??= "";
                model.Price = model.Price;
                model.CreatedDate = model.CreatedDate == default ? DateTime.UtcNow : model.CreatedDate;
                model.IsBilled = false;

                _context.WorkflowTeamOperations.Add(model);
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("workflow-team-operations")]
        public async Task<IActionResult> GetWorkflowTeamOperations()
        {
            var list = await _context.WorkflowTeamOperations
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(list);
        }

        [HttpPut("workflow-team-operations/mark-billed")]
        public async Task<IActionResult> MarkAsBilled([FromBody] List<string> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("Id listesi boş geldi.");

            var list = await _context.WorkflowTeamOperations
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            if (list.Count == 0)
                return BadRequest("Gönderilen id'lerle eşleşen operation kaydı bulunamadı.");

            foreach (var item in list)
            {
                item.IsBilled = true;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                UpdatedCount = list.Count,
                UpdatedIds = list.Select(x => x.Id).ToList()
            });
        }
    }
}