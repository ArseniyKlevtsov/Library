using FluentValidation;
using Library.Application.DTOs.AuthorDtos.Request;

namespace Library.Application.Validators.AutorValidators;

public class AuthorRequestDtoValidator : AbstractValidator<AuthorRequestDto>
{
    public AuthorRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Author's name cannot be empty and must be no longer than 20 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Author's surname cannot be empty and must be no longer than 20 characters.");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(IsValidDate)
            .WithMessage("Author's birth date must be valid.");
    }

    private bool IsValidDate(DateOnly birthDate)
    {
        return birthDate <= DateOnly.FromDateTime(DateTime.Today);
    }
}
