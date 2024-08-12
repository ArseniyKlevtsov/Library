using FluentValidation;
using Library.Application.DTOs.AuthDtos.Request;

namespace Library.Application.Validators.AuthValidators;

public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("The username cannot be empty.")
            .MaximumLength(50)
            .WithMessage("The username must be shorter than 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("The phone number cannot be empty.")
            .Matches(@"^\+?\d{1,3}?[-.\s]?\(?\d{1,3}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}$")
            .WithMessage("The phone number must be in a valid format.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("The email cannot be empty.")
            .EmailAddress()
            .WithMessage("The email must be in a valid format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("The password cannot be empty.")
            .MinimumLength(8)
            .WithMessage("The password must be at least 8 characters long.")
            .Matches("[A-Z]")
            .WithMessage("The password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("The password must contain at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("The password must contain at least one digit.");
    }
}