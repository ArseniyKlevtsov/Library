namespace Library.Application.DTOs.UserDtos.Response;

public class UserResponseDto
{
    public string? UserName { get; set; }
    public string? Login { get; set; }
    public string? PhoneNumber { get; set; }

    public IEnumerable<Guid>? RentOrderIds { get; set; }
}
