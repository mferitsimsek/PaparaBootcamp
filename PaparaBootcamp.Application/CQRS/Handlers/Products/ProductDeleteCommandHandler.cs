using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaparaBootcamp.Application.CQRS.Commands.Product;
using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Application.Validators;


namespace PaparaBootcamp.Application.CQRS.Handlers.Products
{
    public class ProductDeleteCommandHandler:IRequestHandler<ProductDeleteCommandRequest,IActionResult>
    {
        private readonly IProductRepository _productRepository;


        public ProductDeleteCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Handle(ProductDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            ProductDeleteCommandValidator validations = new ProductDeleteCommandValidator();
            validations.ValidateAndThrow(request);
            var product = await _productRepository.GetItemAsync(request.ProductId);
            if (product == null)
            {
                return new NotFoundObjectResult(new { Message = "Ürün bulunamadı." });
            }

            await _productRepository.DeleteAsync(product.Id);

            return new NoContentResult(); // Silme işlemi başarılıysa 204 No Content dönüyoruz
        }
    }
}
