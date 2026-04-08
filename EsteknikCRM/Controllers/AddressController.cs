using EsteknikCRM.Api.Data;
using EsteknikCRM.Api.Entities;
using EsteknikCRM.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound("Adres bulunamadı");

            return Ok(address);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var addresses = await _context.Addresses
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();

            return Ok(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Address address)
        {
            try
            {
                address.Id = Guid.NewGuid().ToString();

                address.AddressLine ??= "";
                address.FlatNo ??= "";
                address.BuildingNo ??= "";
                address.Street ??= "";
                address.Neighborhood ??= "";
                address.District ??= "";
                address.City ??= "";
                address.Country ??= "";
                address.Status ??= "Aktif";
                address.OwnershipType ??= "";
                address.PostCode ??= "";
                address.CreatedDate ??= DateTime.UtcNow;

                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound("Adres bulunamadı");

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }
    }
}