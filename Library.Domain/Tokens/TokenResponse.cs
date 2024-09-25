namespace Library.Domain.Tokens;

public class TokenResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public bool? HasAdminRole { get; set; }
}
