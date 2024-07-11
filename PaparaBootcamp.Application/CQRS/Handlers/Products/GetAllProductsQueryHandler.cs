using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using PaparaBootcamp.Application.CQRS.Queries.Product;
using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.CQRS.Handlers.Product
{
    public class GetAllProductsQueryHandler:IRequestHandler<GetAllProductsQueryRequest,List<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        
        public async Task<List<ProductDTO>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
              var products = await _productRepository.GetAllAsync();
                if (!products.Any())
                {
                    throw new InvalidOperationException("Ürünler bulunamadı.");
                }
                return _mapper.Map<List<ProductDTO>>(products); ;           
        }
    }
}
