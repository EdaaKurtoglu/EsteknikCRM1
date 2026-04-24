using EsteknikCRM.Api.Data;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkflowController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkflowController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workflows = await _context.Workflows
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(workflows);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var workflow = await _context.Workflows.FindAsync(id);

            if (workflow == null)
                return NotFound("İş akışı bulunamadı");

            return Ok(workflow);
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompleted()
        {
            var workflows = await _context.Workflows
                .Where(x => x.WorkflowStatus == "Tamamlandı")
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            return Ok(workflows);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Workflow workflow)
        {
            try
            {
                workflow.Id = Guid.NewGuid().ToString();

                if (string.IsNullOrWhiteSpace(workflow.WorkflowId))
                    workflow.WorkflowId = workflow.Id;

                _context.Workflows.Add(workflow);
                await _context.SaveChangesAsync();

                return Ok(workflow);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Workflow updatedWorkflow)
        {
            var workflow = await _context.Workflows.FindAsync(id);

            if (workflow == null)
                return NotFound("İş akışı bulunamadı");

            workflow.StartType = updatedWorkflow.StartType;
            workflow.CustomerId = updatedWorkflow.CustomerId;
            workflow.CustomerName = updatedWorkflow.CustomerName;
            workflow.CustomerSurname = updatedWorkflow.CustomerSurname;
            workflow.CustomerFullName = updatedWorkflow.CustomerFullName;
            workflow.CustomerPhone = updatedWorkflow.CustomerPhone;

            workflow.AddressId = updatedWorkflow.AddressId;
            workflow.AddressLine = updatedWorkflow.AddressLine;

            workflow.CategoryId = updatedWorkflow.CategoryId;
            workflow.CategoryName = updatedWorkflow.CategoryName;

            workflow.NotificationTypeId = updatedWorkflow.NotificationTypeId;
            workflow.NotificationTypeName = updatedWorkflow.NotificationTypeName;

            workflow.SubCategoryId = updatedWorkflow.SubCategoryId;
            workflow.SubCategoryName = updatedWorkflow.SubCategoryName;

            workflow.DeviceId = updatedWorkflow.DeviceId;
            workflow.DeviceName = updatedWorkflow.DeviceName;

            workflow.Description = updatedWorkflow.Description;
            workflow.ExtraDescription = updatedWorkflow.ExtraDescription;
            workflow.ArrivalChannel = updatedWorkflow.ArrivalChannel;

            workflow.WorkflowStatus = updatedWorkflow.WorkflowStatus;
            workflow.FlowType = updatedWorkflow.FlowType;
            workflow.Subject = updatedWorkflow.Subject;

            workflow.CreatedByUserMail = updatedWorkflow.CreatedByUserMail;
            workflow.CreatedByName = updatedWorkflow.CreatedByName;
            workflow.CreatedBySurname = updatedWorkflow.CreatedBySurname;
            workflow.CreatedByFullName = updatedWorkflow.CreatedByFullName;
            workflow.CreatedByRole = updatedWorkflow.CreatedByRole;
            workflow.WorkTeam = updatedWorkflow.WorkTeam;
            workflow.LastAction = updatedWorkflow.LastAction;

            await _context.SaveChangesAsync();

            return Ok(workflow);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] string status)
        {
            var workflow = await _context.Workflows.FindAsync(id);

            if (workflow == null)
                return NotFound("İş akışı bulunamadı");

            workflow.WorkflowStatus = status;
            await _context.SaveChangesAsync();

            return Ok(workflow);
        }

        [HttpPut("{id}/team")]
        public async Task<IActionResult> UpdateTeam(string id, [FromBody] string team)
        {
            var workflow = await _context.Workflows.FindAsync(id);

            if (workflow == null)
                return NotFound("İş akışı bulunamadı");

            workflow.WorkTeam = team;
            await _context.SaveChangesAsync();

            return Ok(workflow);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var workflow = await _context.Workflows.FindAsync(id);

            if (workflow == null)
                return NotFound("İş akışı bulunamadı");

            _context.Workflows.Remove(workflow);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }

    }
}