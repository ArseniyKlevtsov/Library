using Library.Domain.Entities;
using Library.Domain.Tokens;

namespace Library.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    Task<TokenResponse> GenerateTokensAsync(User user);
    Task<User?> GetUserFromTokenAsync(string token);
    Task SetRefreshTokenAsync(User user, string refreshToken);
}
