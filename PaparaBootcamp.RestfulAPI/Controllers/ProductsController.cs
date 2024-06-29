using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? name, string? sortBy)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "name" => query.OrderBy(p => p.Name),
                    "price" => query.OrderBy(p => p.Price),
                    _ => query
                };
            }

            return Ok(await query.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult> GetItems(int pageIndex = 0, int pageSize = 10)
        {
            var items = _context.Products
               .Skip(pageIndex * pageSize)
               .Take(pageSize)
               .ToList();

            var totalCount =  _context.Products.Count();

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

        [HttpGet]
        public async Task<ActionResult<Product>> GetProduct([FromQuery]int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new { Message = "Ürün bulunamadı." });
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct([FromQuery]int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new { Message = "Güncellemek istediğiniz ürün id leri eşleşmedi." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Product>> PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(new { Message = "Geçersiz yama dokümanı." });
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { Message = "Ürün bulunamadı." });
            }

            patchDoc.ApplyTo(product,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
