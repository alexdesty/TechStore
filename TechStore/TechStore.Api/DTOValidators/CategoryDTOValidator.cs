using FluentValidation;
using TechStore.Api.DTO;
using TechStore.Domain.Entities;
namespace TechStore.Api.DTOValidators;

public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
{
    public CategoryDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required")
            .Length(2, 50)
            .WithMessage("Category name must be between 2 and 50 characters");
    }
}
