using FluentValidation;
using TechStore.Api.DTO;

namespace TechStore.Api.DTOValidators;

public class ProductToPropertyDTOValidator : AbstractValidator<ProductToPropertyDTO>
{
    public ProductToPropertyDTOValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Property value is required")
            .Length(1, 50)
            .WithMessage("Property value must be between 1 and 50 characters");
    }
}
