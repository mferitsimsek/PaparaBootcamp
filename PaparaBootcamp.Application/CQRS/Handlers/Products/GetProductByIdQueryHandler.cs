using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PaparaBootcamp.Application.CQRS.Queries.Product;
using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Application.Validators;
using PaparaBootcamp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.CQRS.Handlers.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {

            GetProductByIdQueryValidator validations = new GetProductByIdQueryValidator();
            validations.ValidateAndThrow(request);
            var product = await _productRepository.GetItemAsync(request.ProductId);

            if (product == null)
            {
                throw new InvalidOperationException("Ürün bulunamadı.");
            }
            return _mapper.Map<ProductDTO>(product);


        }
    }
}
