using EsteknikCRM.Api.Data;
using EsteknikCRM.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsteknikCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductLaborPricesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductLaborPricesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.ProductLaborPrices
                .OrderBy(x => x.ProductCode)
                .ThenBy(x => x.LaborCode)
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductLaborPrice model)
        {
            try
            {
                model.Id = Guid.NewGuid().ToString();
                model.Currency ??= "TRY";
                model.Status ??= "active";
                model.CreatedDate = DateTime.UtcNow;

                _context.ProductLaborPrices.Add(model);
                await _context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ProductLaborPrice updated)
        {
            var item = await _context.ProductLaborPrices.FindAsync(id);

            if (item == null)
                return NotFound("Fiyat kaydı bulunamadı.");

            item.ProductCode = updated.ProductCode;
            item.StockCode = updated.StockCode;
            item.ProductName = updated.ProductName;
            item.LaborCode = updated.LaborCode;
            item.LaborName = updated.LaborName;
            item.SubLaborName = updated.SubLaborName;
            item.Price = updated.Price;
            item.Currency = updated.Currency ?? "TRY";
            item.Status = updated.Status ?? "active";

            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _context.ProductLaborPrices.FindAsync(id);

            if (item == null)
                return NotFound("Fiyat kaydı bulunamadı.");

            _context.ProductLaborPrices.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Silindi");
        }
    }
}