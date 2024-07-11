using FluentValidation;
using PaparaBootcamp.Application.CQRS.Commands.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Validators
{
    public class ProductUpdateCommandValidator : AbstractValidator<ProductUpdateCommandRequest>
    {
        public ProductUpdateCommandValidator()
        {
            //Product id 0 dan küçük gelirse hata alacağı için 0 dan büyük şartı konuldu.
            RuleFor(request => request.Id).GreaterThan(0);
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command=>command.Name).MinimumLength(3);
            RuleFor(command => command.Price).GreaterThan(1000);
            RuleFor(command => command.UpdatedDate).GreaterThan(DateTime.Now.AddDays(-1));
        }
    }
}
