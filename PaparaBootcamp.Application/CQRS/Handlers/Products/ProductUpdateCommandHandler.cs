using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaparaBootcamp.Application.CQRS.Commands.Product;
using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Application.Validators;
using PaparaBootcamp.Domain.DTOs;
using PaparaBootcamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.CQRS.Handlers.Products
{
    public class ProductUpdateCommandHandler:IRequestHandler<ProductUpdateCommandRequest,ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductUpdateCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<ProductDTO> Handle(ProductUpdateCommandRequest request,CancellationToken cancellationToken)
        {
            ProductUpdateCommandValidator validations = new ProductUpdateCommandValidator();
            validations.ValidateAndThrow(request);
            var product = await _productRepository.GetItemAsync(request.Id);

            if (product == null)
            {
                throw new InvalidOperationException("Ürünler bulunamadı.");
            }
            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.UpdatedDate=request.UpdatedDate;
            



            await _productRepository.UpdateAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
