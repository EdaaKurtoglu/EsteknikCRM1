using EsteknikCRM.Api.Data;
using Microsoft.AspNetCore.Mvc;
using EsteknikCRM.Entities;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Product product)
    {
        try
        {
            product.Id = Guid.NewGuid().ToString();
            product.ProductCode ??= "";
            product.ProductNameTr ??= "";
            product.ProductNameEn ??= "";
            product.CostCenter ??= "";
            product.BCode ??= "";
            product.ProductManager ??= "";
            product.Brand ??= "";
            product.TopGroup ??= "";
            product.SubGroup ??= "";
            product.SpecialGroup ??= "";
            product.Country ??= "";
            product.ProductionPlace ??= "";
            product.SalesInfo ??= "";
            product.Status ??= "active";
            product.CreatedDate = product.CreatedDate == default ? DateTime.UtcNow : product.CreatedDate;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
        }
    }
}