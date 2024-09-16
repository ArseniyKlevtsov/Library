using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.AuthDtos.Response;

namespace Library.Application.Interfaces.UseCases.Auth;

public interface IRefreshAuthorizationUseCase
{
    Task<TokenResponse> ExecuteAsync(RefreshRequestDto refreshRequestDto);
}
