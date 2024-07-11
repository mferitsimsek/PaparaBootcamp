using MediatR;
using PaparaBootcamp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.CQRS.Queries.Product
{
    public class GetProductsQueryRequest:IRequest<(List<ProductDTO> Items,int TotalCount)>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string SortBy { get; set; }


    }
}
