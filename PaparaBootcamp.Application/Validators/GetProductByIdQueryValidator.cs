using FluentValidation;
using PaparaBootcamp.Application.CQRS.Queries.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Validators
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQueryRequest>
    {
        public GetProductByIdQueryValidator()
        {
            //Product id 0 dan küçük gelirse hata alacağı için 0 dan büyük şartı konuldu.
            RuleFor(request => request.ProductId).GreaterThan(0);
        }
    }
}


