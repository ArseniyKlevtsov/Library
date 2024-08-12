using Library.Domain.Entities;

namespace Library.Application.DTOs.UserDtos.Response;

public class UserResponseDto
{
    public string? UserName { get; set; }
    public string? Login { get; set; }
    public string? PhoneNumber { get; set; }

    public IEnumerable<RentOrder>? RentOrders { get; set; }
}
