using EsteknikCRM.Api.Data;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DeviceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var devices = await _context.Devices.ToListAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return Ok(device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Device updatedDevice)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
                return NotFound();

            device.SerialNumber = updatedDevice.SerialNumber;
            device.DeviceCode = updatedDevice.DeviceCode;
            device.DeviceName = updatedDevice.DeviceName;
            device.Brand = updatedDevice.Brand;
            device.TopGroup = updatedDevice.TopGroup;
            device.SubGroup = updatedDevice.SubGroup;
            device.SpecialGroup = updatedDevice.SpecialGroup;
            device.Status = updatedDevice.Status;
            device.CommissionDate = updatedDevice.CommissionDate;

            await _context.SaveChangesAsync();

            return Ok(device);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
                return NotFound();

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }
    }
}
