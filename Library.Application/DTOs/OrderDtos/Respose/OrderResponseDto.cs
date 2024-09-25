using Library.Application.DTOs.RentedBookDtos.Response;

namespace Library.Application.DTOs.OrderDtos.Respose;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public IEnumerable<RentedbookResponseDto>? RentedBooks { get; set; }
}
