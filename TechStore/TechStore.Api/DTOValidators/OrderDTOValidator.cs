using FluentValidation;
using TechStore.Api.DTO;

namespace TechStore.Api.DTOValidators;

public class OrderDTOValidator : AbstractValidator<OrderDTO>
{
    public OrderDTOValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("Order userId is required");
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Order userId is required");
        RuleFor(x => x.DeliveryPhoneNumber)
            .NotEmpty()
            .WithMessage("Delivery phone number is required");
        RuleFor(x => x.DeliveryStatus)
            .NotEmpty()
            .WithMessage("Delivery status is required");
    }
}
