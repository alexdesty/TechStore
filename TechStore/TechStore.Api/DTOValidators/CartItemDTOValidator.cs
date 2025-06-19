using FluentValidation;
using TechStore.Api.DTO;

namespace TechStore.Api.DTOValidators;

public class CartItemDTOValidator : AbstractValidator<CartItemDTO>
{
    public CartItemDTOValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("CartId is required");
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId status is required");
        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Produc amounts is required");
    }
}
