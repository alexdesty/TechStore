using FluentValidation;
using TechStore.Api.DTO;

namespace TechStore.Api.DTOValidators;

public class CartItemToCreateDTOValidator : AbstractValidator<CartItemToCreateDTO>
{
    public CartItemToCreateDTOValidator()
    {
        RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId status is required");
        RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("Produc amounts is required");
    }
}

