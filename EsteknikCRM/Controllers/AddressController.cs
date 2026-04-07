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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var address = await _context.Addresses.ToListAsync();
            return Ok(address);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Address updatedAddress)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound();

            address.CustomerId = updatedAddress.CustomerId;
            address.AddressLine = updatedAddress.AddressLine;
            address.FlatNo = updatedAddress.FlatNo;
            address.BuildingNo = updatedAddress.BuildingNo;
            address.Street = updatedAddress.Street;
            address.Neighborhood =  updatedAddress.Neighborhood;
            address.District = updatedAddress.District;
            address.City = updatedAddress.City;
            address.Country = updatedAddress.Country;
            address.IsResidence = updatedAddress.IsResidence;
            address.Status = updatedAddress.Status;
            address.OwnershipType = updatedAddress.OwnershipType;
            address.PostCode = updatedAddress.PostCode;
            address.IsActive = updatedAddress.IsActive;
            address.CreatedDate = updatedAddress.CreatedDate;
            address.PassiveDate = updatedAddress.PassiveDate;
            //address.IsResidenceText = updatedAddress.IsResidenceText;
            

            await _context.SaveChangesAsync();

            return Ok(address);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
                return NotFound();

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }
    }
}
