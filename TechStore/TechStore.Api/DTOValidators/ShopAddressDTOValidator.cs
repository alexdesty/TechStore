using FluentValidation;
using TechStore.Api.DTO;
using TechStore.Domain.Entities;

namespace TechStore.Api.DTOValidators
{
    public class ShopAddressDTOValidator : AbstractValidator<ShopAddressDTO>
    {
        public ShopAddressDTOValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required")
                .Length(2, 50)
                .WithMessage("Address must be between 2 and 50 characters");
        }
    }
}
