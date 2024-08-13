using Library.Application.DTOs.AuthDtos.Response;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Services;

public interface ITokenService
{
    Task<TokenResponse> GenerateTokensAsync(User user);
    Task<User?> GetUserFromTokenAsync(string token);
}
