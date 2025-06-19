using FluentValidation;
using TechStore.Api.DTO;

namespace TechStore.Api.DTOValidators;

public class ProductDTOValidator : AbstractValidator<ProductDTO>
{
    public ProductDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .Length(2, 50)
            .WithMessage("Product name must be between 2 and 50 characters");

        RuleFor(x => x.Description)
    .NotEmpty()
    .WithMessage("Description is required")
    .Length(2, 500)
    .WithMessage("Description must be between 2 and 500 characters");

        RuleFor(x => x.Price)
    .NotEmpty()
    .WithMessage("Product price is required")
    .GreaterThan(0)
    .WithMessage("Price must be greater then 0")
    .PrecisionScale(8, 2, false)
    .WithMessage("Зrice must consist of no more than 6 digits before the decimal point and 2 digits after the decimal point");



    }
}
