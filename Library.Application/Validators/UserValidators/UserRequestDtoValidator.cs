using FluentValidation;
using Library.Application.DTOs.UserDtos.Request;

namespace Library.Application.Validators.UserValidators;

public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
    public UserRequestDtoValidator()
    {
        RuleFor(x => x.NewUserName)
            .NotEmpty()
            .WithMessage("The username cannot be empty.")
            .MaximumLength(50)
            .WithMessage("The user name cannot be longer 50 characters");

        RuleFor(x => x.NewPhoneNumber)
            .NotEmpty()
            .WithMessage("The phone number cannot be empty.")
            .Matches(@"^\+?\d{1,3}?[-.\s]?\(?\d{1,3}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,4}$")
            .WithMessage("The phone number must be in a valid format.");

        RuleFor(x => x.NewEmail)
            .NotEmpty()
            .WithMessage("The email cannot be empty.")
            .EmailAddress()
            .WithMessage("The email must be in a valid format.");

        RuleFor(x => x.NewPassword)
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
