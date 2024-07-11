using FluentValidation;
using PaparaBootcamp.Application.CQRS.Commands.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Validators
{
    public class ProductCreateCommandValidator:AbstractValidator<ProductCreateCommandRequest>
    {
        public ProductCreateCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty(); 
            RuleFor(command=>command.Name).MinimumLength(3); // Min uzunluk 3 girince aslında üstteki notEmpty gereksiz ancak boş girince boş uyarısı 3 den az girince ayrı uyarı vermesi için ekledik.
            RuleFor(command => command.Price).GreaterThan(1000);
        }
    }
}
