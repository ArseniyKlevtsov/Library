using Library.Application.DTOs.OrderDtos.Respose;
using Library.Application.DTOs.RentedBookDtos.Request;
using Library.Application.DTOs.RentedBookDtos.Response;
using Library.Application.Interfaces.UseCases.ProfileUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : Controller
{
    private IGetUserOrders _getUserOrders;

    public ProfileController(IGetUserOrders getUserOrders)
    {
        _getUserOrders = getUserOrders;
    }

    [HttpPost("getUserRents")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<RentedBookProfileResponseDto>>> GetUserOrders(
        GetUserOrdersRequestDto getUserOrdersRequestDto,
        CancellationToken cancellationToken)
    {
        var rentedBooks = await _getUserOrders.ExecuteAsync(getUserOrdersRequestDto, cancellationToken);
        return Ok(rentedBooks);
    }
}
