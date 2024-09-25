using Library.Application.DTOs.RentedBookDtos.Request;

namespace Library.Application.DTOs.OrderDtos.Request;

public class PlaceOrderRequestDto
{
    public string? AccessToken { get; set; }

    public IEnumerable<RentedBookRequestDto>? RentedBooks { get; set; }

}
