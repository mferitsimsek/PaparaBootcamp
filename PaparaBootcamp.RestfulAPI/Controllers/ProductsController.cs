using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaparaBootcamp.RestfulAPI.Context;
using PaparaBootcamp.RestfulAPI.Entities;

namespace PaparaBootcamp.RestfulAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? name)
        {
            if (name != null)
            {
                return Ok(await _context.Products.Where(p => p.Name.Contains(name)).ToListAsync());
            }

            return Ok(await _context.Products.ToListAsync());
        }
        [HttpGet]
        public IActionResult GetItems(int pageIndex = 0, int pageSize = 10)
        {
            var items = _context.Products
               .Skip(pageIndex * pageSize)
               .Take(pageSize)
               .ToList();

            var totalCount = _context.Products.Count();

            var paginationMetadata = new
            {
                totalCount,
                pageIndex,
                pageSize,
                hasNextPage = totalCount > (pageIndex + 1) * pageSize,
                hasPreviousPage = pageIndex > 0
            };

            return Ok(new { items, paginationMetadata });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            if (product.Name == null || product.Description == null || product.Price == 0)
            {
                return BadRequest(new {Message="İsim , Açıklama ve Fiyat Bilgisi girmeniz gerekmektedir."});
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new { Message = "Güncellemek istediğiniz ürün id leri eşleşmedi. + " });
            }

            if (product.Name == null || product.Description == null || product.Price == 0)
            {
                return BadRequest(new { Message = "İsim , Açıklama ve Fiyat Bilgisi girmeniz gerekmektedir." });
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new {Message="Ürün bulunamadı."});
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
