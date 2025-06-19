using FluentValidation;
using TechStore.Api.DTO;

namespace TechStore.Api.DTOValidators;

public class CartDTOValidator : AbstractValidator<CartDTO>
{
    public CartDTOValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");
    }
}
