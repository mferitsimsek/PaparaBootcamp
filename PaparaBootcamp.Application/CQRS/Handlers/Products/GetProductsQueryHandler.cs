using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, (List<ProductDTO> Items, int TotalCount)>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<(List<ProductDTO> Items, int TotalCount)> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            GetProductsQueryValidator validations = new GetProductsQueryValidator();
            validations.ValidateAndThrow(request);
            var (items, totalCount) =  await _productRepository.GetItemsAsync(request.PageIndex,request.PageSize,request.Name,request.SortBy);
            var itemsDTO = _mapper.Map<List<ProductDTO>>(items);

            return (itemsDTO, totalCount);
        }
    }
}
