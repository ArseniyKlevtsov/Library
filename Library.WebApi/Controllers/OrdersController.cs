using Library.Application.DTOs.OrderDtos.Request;
using Library.Application.DTOs.OrderDtos.Respose;
using Library.Application.Interfaces.UseCases.OrderUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : Controller
{
    private IPlaceOrder _placeOrder;
    public OrdersController(IPlaceOrder placeOrder)
    {
        _placeOrder = placeOrder;
    }

    [HttpPost("placeOrder")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<OrderResponseDto>> PlaceOrder(PlaceOrderRequestDto placeOrderRequestDto, CancellationToken cancellationToken)
    {
        var createdOrder = await _placeOrder.ExecuteAsync(placeOrderRequestDto, cancellationToken);
        return Ok(createdOrder);
    }
}
