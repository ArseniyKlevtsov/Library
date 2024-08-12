using FluentValidation;
using Library.Application.DTOs.GenreDtos.Request;

namespace Library.Application.Validators.GenreValidators;

public class GenreRequestDtoValidator : AbstractValidator<GenreRequestDto>
{
    public GenreRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Name cannot be empty and must be no longer than 20 characters.");
    }
}
