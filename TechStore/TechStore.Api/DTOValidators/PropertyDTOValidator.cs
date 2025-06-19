using FluentValidation;
using TechStore.Api.DTO;

namespace TechStore.Api.DTOValidators;

public class PropertyDTOValidator : AbstractValidator<PropertyDTO>
{
    public PropertyDTOValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("Category name is required")
        .Length(2, 25)
        .WithMessage("Category name must be between 2 and 25 characters");
    }
}
