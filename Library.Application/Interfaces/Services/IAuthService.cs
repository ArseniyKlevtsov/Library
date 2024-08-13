using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.AuthDtos.Response;

namespace Library.Application.Interfaces.Services;

public interface IAuthService
{
    Task<TokenResponse> LoginAsync(LoginRequestDto loginRequestDto);
    Task RegisterAsync(RegisterRequestDto registerRequestDto);
    Task<TokenResponse> RefreshTokenAsync(RefreshRequestDto refreshRequestDto);
}
