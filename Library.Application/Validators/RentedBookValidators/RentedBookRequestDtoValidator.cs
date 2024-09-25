using FluentValidation;
using Library.Application.DTOs.RentedBookDtos.Request;

namespace Library.Application.Validators.RentedBookValidators;

public class RentedBookRequestDtoValidator: AbstractValidator<RentedBookRequestDto>
{
    public RentedBookRequestDtoValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("BookId cannot be empty.");

        RuleFor(x => x.BooksCount)
            .NotEmpty()
            .WithMessage("BooksCount cannot be empty.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("BooksCount must be at least 1.");

        RuleFor(x => x.TakeTime)
            .NotEmpty()
            .WithMessage("TakeTime cannot be empty.");
    }
}
