using FluentValidation;
using Library.Application.DTOs.BookDtos.Request;

namespace Library.Application.Validators.BookValidators;

public class BookRequestDtoValidator : AbstractValidator<BookRequestDto>
{
    public BookRequestDtoValidator()
    {
        RuleFor(x => x.Isbn)
            .NotEmpty()
            .Matches(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")
            .WithMessage("ISBN number must be a valid 10 or 13-digit ISBN number.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Book name cannot be empty and must be no longer than 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Book description cannot be empty and must be no longer than 1000 characters.");
    }
}
