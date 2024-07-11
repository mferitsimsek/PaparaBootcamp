using MediatR;
using PaparaBootcamp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.CQRS.Commands.Product
{
    public class ProductCreateCommandRequest:IRequest<ProductDTO>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        
    }
}
