namespace Library.Application.DTOs.AuthDtos.Request;

public class RefreshRequestDto
{
    public string? ExpiredAccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
