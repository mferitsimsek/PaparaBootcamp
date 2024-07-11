using MediatR;
using PaparaBootcamp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.CQRS.Queries.Product
{
    public class GetProductByIdQueryRequest:IRequest<ProductDTO>
    {
        public int ProductId { get; set; }
    }
}
