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

        [HttpGet("labor-operations")]
        public async Task<IActionResult> GetLaborOperations()
        {
            var data = await _context.LaborOperations
                .Where(x => x.Status == "active")
                .OrderBy(x => x.LaborCode)
                .ToListAsync();

            return Ok(data);
        }
        // 🔹 fiyat çekme
        [HttpGet("price")]
        public async Task<IActionResult> GetPrice([FromQuery] string productCode, [FromQuery] string laborCode)
        {
            var price = await _context.ProductLaborPrices
                .Where(x => x.ProductCode == productCode && x.LaborCode == laborCode && x.Status == "active")
                .Select(x => x.Price)
                .FirstOrDefaultAsync();

            return Ok(price);
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
        public async Task<IActionResult> MarkAsBilled([FromBody] MarkBilledRequest request)
        {
            var list = await _context.WorkflowTeamOperations
                .Where(x => request.OperationIds.Contains(x.Id))
                .ToListAsync();

            foreach (var item in list)
            {
                item.IsBilled = true;
                item.HakedisSetId = request.HakedisSetId;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("workflow-team-operations/by-hakedis-set/{setId}")]
        public async Task<IActionResult> GetOperationsByHakedisSet(string setId)
        {
            var list = await _context.WorkflowTeamOperations
                .Where(x => x.HakedisSetId == setId)
                .ToListAsync();

            return Ok(list);
        }
        
        [HttpPut("approve")]
        public async Task<IActionResult> ApproveOperation([FromBody] ApproveRequest request)
        {
            var operation = await _context.WorkflowTeamOperations
                .FirstOrDefaultAsync(x => x.Id == request.OperationId);

            if (operation == null)
                return NotFound();

            operation.ApprovalStatus = request.Status;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
    public class MarkBilledRequest
    {
        public string HakedisSetId { get; set; }
        public List<string> OperationIds { get; set; }
    }
    public class ApproveRequest
    {
        public string OperationId { get; set; }
        public int Status { get; set; } // 1 = onay, 2 = red
    }
}