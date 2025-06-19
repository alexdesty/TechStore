using FluentValidation;
using TechStore.Api.DTO;

namespace TechStore.Api.DTOValidators;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("First name is required")
            .Length(2, 50)
            .WithMessage("First name must be between 2 and 50 characters")
            .Must(x => !x.Any(char.IsDigit))
            .WithMessage("First name shoud not contain numbers");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Surname is required")
            .Length(2, 50)
            .WithMessage("Surname name must be between 2 and 50 characters")
            .Must(x => !x.Any(char.IsDigit))
            .WithMessage("First name shoud not contain numbers");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Incorrect email address");

        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required")
            .Length(2, 50)
            .WithMessage("Login must be between 2 and 50 characters");

        RuleFor(x=>x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Length(2, 50)
            .WithMessage("Password must be between 2 and 50 characters");   
    }
}
