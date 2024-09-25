using Library.Application.DTOs.OrderDtos.Respose;
using Library.Application.DTOs.RentedBookDtos.Request;
using Library.Application.DTOs.RentedBookDtos.Response;

namespace Library.Application.Interfaces.UseCases.ProfileUseCases;

public interface IGetUserOrders
{
    Task<IEnumerable<RentedBookProfileResponseDto>> ExecuteAsync(GetUserOrdersRequestDto getUserOrdersDto, CancellationToken cancellationToken);
}
