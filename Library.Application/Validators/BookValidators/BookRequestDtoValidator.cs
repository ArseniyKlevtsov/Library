using FluentValidation;
using Library.Application.DTOs.BookDtos.Request;

namespace Library.Application.Validators.BookValidators;

public class BookRequestDtoValidator : AbstractValidator<BookRequestDto>
{
    private readonly string isbnRegex = @"^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$";
    public BookRequestDtoValidator()
    {
        RuleFor(x => x.Isbn)
            .NotEmpty()
            .Matches(isbnRegex)
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
