using FluentValidation;
using PaparaBootcamp.Application.CQRS.Queries.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Validators
{
    public class GetProductsQueryValidator:AbstractValidator<GetProductsQueryRequest>
    {

        public GetProductsQueryValidator()
        {
            //Pagination kontrolleri eklendi.
            RuleFor(request=>request.PageIndex).NotEmpty();
            RuleFor(request=>request.PageIndex).GreaterThan(-1);            
            RuleFor(request=>request.PageSize).NotEmpty();
            RuleFor(request=>request.PageSize).GreaterThan(1);
        }
    }
}
