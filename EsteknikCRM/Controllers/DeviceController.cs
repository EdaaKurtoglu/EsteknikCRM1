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
            try
            {
                var devices = await _context.Devices
                    .AsNoTracking()
                    .Select(x => new
                    {
                        x.Id,
                        x.SerialNumber,
                        x.DeviceCode,
                        x.DeviceName,
                        x.CommissionDate,
                        x.Brand,
                        x.TopGroup,
                        x.SubGroup,
                        x.SpecialGroup,
                        x.Status,
                        x.StockCode
                    })
                    .ToListAsync();

                return Ok(devices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var device = await _context.Devices
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => new
                    {
                        x.Id,
                        x.SerialNumber,
                        x.DeviceCode,
                        x.DeviceName,
                        x.CommissionDate,
                        x.Brand,
                        x.TopGroup,
                        x.SubGroup,
                        x.SpecialGroup,
                        x.Status,
                        x.StockCode
                    })
                    .FirstOrDefaultAsync();

                if (device == null)
                    return NotFound();

                return Ok(device);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add(Device device)
        {
            try
            {
                device.Id = Guid.NewGuid().ToString();

                device.SerialNumber ??= "";
                device.DeviceCode ??= "";
                device.DeviceName ??= "";
                device.Brand ??= "";
                device.TopGroup ??= "";
                device.SubGroup ??= "";
                device.SpecialGroup ??= "";
                device.Status ??= "Aktif";

                _context.Devices.Add(device);
                await _context.SaveChangesAsync();

                return Ok(device);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Device updatedDevice)
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
        public async Task<IActionResult> Delete(string id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
                return NotFound();

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBySerialOrStockCode([FromQuery] string? serialNo, [FromQuery] string? stockCode)
        {
            serialNo = serialNo?.Trim();
            stockCode = stockCode?.Trim();

            if (string.IsNullOrWhiteSpace(serialNo) && string.IsNullOrWhiteSpace(stockCode))
                return BadRequest("Seri numarası veya stok kodu girilmelidir.");

            var query = _context.Devices.AsQueryable();

            if (!string.IsNullOrWhiteSpace(serialNo) && !string.IsNullOrWhiteSpace(stockCode))
            {
                var device = await query.FirstOrDefaultAsync(x =>
                    x.SerialNumber == serialNo &&
                    x.StockCode == stockCode);

                if (device == null)
                    return NotFound("Seri numarası ve stok kodu birlikte eşleşen cihaz bulunamadı.");

                return Ok(device);
            }

            if (!string.IsNullOrWhiteSpace(serialNo))
            {
                var device = await query.FirstOrDefaultAsync(x => x.SerialNumber == serialNo);

                if (device == null)
                    return NotFound("Seri numarasına ait cihaz bulunamadı.");

                return Ok(device);
            }

            var stockDevice = await query.FirstOrDefaultAsync(x => x.DeviceCode == stockCode);

            if (stockDevice == null)
                return NotFound("Stok koduna ait cihaz bulunamadı.");

            return Ok(stockDevice);
        }
    }
}
