using FluentValidation;
using Library.Application.DTOs.OrderDtos.Request;

namespace Library.Application.Validators.OrderValidators;

public class PlaceOrderRequestDtoValidator: AbstractValidator<PlaceOrderRequestDto>
{
    public PlaceOrderRequestDtoValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .WithMessage("AccessToken cannot be empty.");

        RuleFor(x => x.RentedBooks)
            .NotEmpty()
            .WithMessage("RentedBooks cannot be empty.");
    }
}
