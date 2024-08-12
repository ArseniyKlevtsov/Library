namespace Library.Application.DTOs.AuthDtos.Response;

public class TokenResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
