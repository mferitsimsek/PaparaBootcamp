using FluentValidation;
using PaparaBootcamp.RestfulAPI.Entities;

namespace PaparaBootcamp.RestfulAPI.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim gereklidir.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Açıklama gereklidir.");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Fiyat sıfırdan büyük olmalıdır.");
        }
    }
}
