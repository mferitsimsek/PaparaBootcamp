using AutoMapper;
using FluentValidation;
using MediatR;
using PaparaBootcamp.Application.CQRS.Commands.Product;
using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Application.Validators;
using PaparaBootcamp.Domain.DTOs;
using PaparaBootcamp.Domain.Entities;


namespace PaparaBootcamp.Application.CQRS.Handlers.Products
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, ProductDTO>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductCreateCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        
        public async Task<ProductDTO> Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
        {
            ProductCreateCommandValidator validations = new ProductCreateCommandValidator();
            validations.ValidateAndThrow(request);
            ProductEntity product = new ProductEntity
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.Now
            };
            if (product==null)
            {
                throw new InvalidOperationException("Ürünler bulunamadı.");
            }
            await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
