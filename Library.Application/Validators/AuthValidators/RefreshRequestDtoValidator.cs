using FluentValidation;
using Library.Application.DTOs.AuthDtos.Request;

namespace Library.Application.Validators.AuthValidators;

public class RefreshRequestDtoValidator : AbstractValidator<RefreshRequestDto>
{
    public RefreshRequestDtoValidator()
    {
        RuleFor(x => x.ExpiredAccessToken)
            .NotEmpty()
            .WithMessage("The ExpiredAccessToken cannot be empty.");

        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("The RefreshToken cannot be empty.");
    }
}
