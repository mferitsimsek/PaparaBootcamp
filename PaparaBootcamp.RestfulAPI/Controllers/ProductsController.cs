using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PaparaBootcamp.Application.Attributes;
using PaparaBootcamp.Application.Services;
using PaparaBootcamp.Domain.DTOs;
using PaparaBootcamp.Domain.Entities;
using PaparaBootcamp.Persistence.Context;
using System.Runtime.InteropServices;

namespace PaparaBootcamp.RestfulAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(MyDbContext context, ProductService productService, IMapper mapper)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(string? name, string? sortBy)
        {
            var products = await _productService.GetProductsAsync(name, sortBy);
            return Ok(products);
        }

        [HttpGet("items")]
        public async Task<ActionResult> GetItems(int pageIndex = 0, int pageSize = 10)
        {
            var (items, totalCount) = await _productService.GetItemsAsync(pageIndex, pageSize);
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
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                return NotFound(new { Message = "Ürün bulunamadı." });
            }

            return Ok(product);
        }

        [HttpPost]
        [Auth]
        public async Task<ActionResult<Product>> PostProduct([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(productDTO);

            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Auth]
        public async Task<ActionResult<Product>> PutProduct(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest(new { Message = "Güncellemek istediğiniz ürün id'leri eşleşmedi." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product= _mapper.Map<Product>(productDTO);  
            await _productService.UpdateProductAsync(product);
            return Ok(product);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Product>> PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(new { Message = "Geçersiz yama dokümanı." });
            }

            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound(new { Message = "Ürün bulunamadı." });
            }

            patchDoc.ApplyTo(product, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.UpdateProductAsync(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound(new { Message = "Ürün bulunamadı." });
            }

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

    }
}
