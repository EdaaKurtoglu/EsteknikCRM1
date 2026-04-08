using EsteknikCRM.Api.Data;
using EsteknikCRM.Api.Entities;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerDeviceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerDeviceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetDevicesByCustomerId(string customerId)
        {
            var deviceIds = await _context.CustomerDevices
                .Where(x => x.CustomerId == customerId && x.IsActive)
                .Select(x => x.DeviceId)
                .ToListAsync();

            var devices = await _context.Devices
                .Where(d => deviceIds.Contains(d.Id))
                .ToListAsync();

            return Ok(devices);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerDevice model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.IsActive = true;
                model.CreatedDate = DateTime.UtcNow;

                _context.CustomerDevices.Add(model);
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
            var relation = await _context.CustomerDevices.FindAsync(id);

            if (relation == null)
                return NotFound("Kayıt bulunamadı");

            _context.CustomerDevices.Remove(relation);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }
    }
}