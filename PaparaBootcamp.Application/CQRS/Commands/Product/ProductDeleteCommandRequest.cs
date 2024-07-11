using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaparaBootcamp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PaparaBootcamp.Application.CQRS.Commands.Product
{
    public class ProductDeleteCommandRequest:IRequest<IActionResult>
    {
        public int ProductId { get; set; }
    }
}
