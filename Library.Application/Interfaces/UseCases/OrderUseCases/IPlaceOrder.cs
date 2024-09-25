using Library.Application.DTOs.OrderDtos.Request;
using Library.Application.DTOs.OrderDtos.Respose;

namespace Library.Application.Interfaces.UseCases.OrderUseCases;

public interface IPlaceOrder
{
    Task<OrderResponseDto> ExecuteAsync(PlaceOrderRequestDto placeOrderRequestDto, CancellationToken cancellationToken);
}
