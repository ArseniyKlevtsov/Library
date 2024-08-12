using FluentValidation;
using Library.Application.DTOs.AuthDtos.Request;

namespace Library.Application.Validators.AuthValidators;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{

    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("The username cannot be empty.")
            .MaximumLength(50)
            .WithMessage("The user name cannot be longer 50 characters");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("The password cannot be empty.")
            .MinimumLength(8);

    }
}
