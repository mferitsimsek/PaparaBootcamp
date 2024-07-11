using AutoMapper;
using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PaparaBootcamp.Application.Attributes;
using PaparaBootcamp.Application.CQRS.Commands.Product;
using PaparaBootcamp.Application.CQRS.Queries.Product;
using PaparaBootcamp.Application.Services;
using PaparaBootcamp.Application.Validators;
using PaparaBootcamp.Domain.DTOs;
using PaparaBootcamp.Domain.Entities;
using PaparaBootcamp.Persistence.Context;
using PaparaBootcamp.RestfulAPI.Entities;
using System.Globalization;
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
        private readonly IMediator _mediator;

        public ProductsController(MyDbContext context, ProductService productService, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _productService = productService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            try
            {
                var response = await _mediator.Send(request: new GetAllProductsQueryRequest());
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("items")]
        public async Task<ActionResult> GetProducts(GetProductsQueryRequest request)
        {
            try
            {
                var response = await _mediator.Send(request);
                // Pagination kontrollerini response'u döndürmeden yapıyoruz.
                var paginationMetadata = new
                {
                    response.TotalCount,
                    request.PageIndex,
                    request.PageSize,
                    hasNextPage = response.TotalCount > (request.PageIndex + 1) * request.PageSize,
                    hasPreviousPage = request.PageIndex > 0
                };
                return Ok(new { response.Items, paginationMetadata });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            try
            {
                
                var response = await _mediator.Send(new GetProductByIdQueryRequest { ProductId = id });
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Validasyonlar Handler sınıfları içerisinde yapılıyor. Hata durumunda buraya düşer.
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Auth]
        public async Task<IActionResult> PostProduct(ProductCreateCommandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                var response = await _mediator.Send<ProductDTO>(request);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Auth]
        public async Task<ActionResult<ProductEntity>> PutProduct(ProductUpdateCommandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {              
                var response = await _mediator.Send<ProductDTO>(request);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ProductEntity>> PatchProduct(int id, [FromBody] JsonPatchDocument<ProductEntity> patchDoc)
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

            await _productService.UpdateAsync(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var response = await _mediator.Send(new ProductDeleteCommandRequest { ProductId = id });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

    }
}
