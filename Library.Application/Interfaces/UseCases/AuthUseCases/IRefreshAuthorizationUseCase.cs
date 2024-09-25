using Library.Application.DTOs.AuthDtos.Request;
using Library.Domain.Tokens;


namespace Library.Application.Interfaces.UseCases.Auth;

public interface IRefreshAuthorizationUseCase
{
    Task<TokenResponse> ExecuteAsync(RefreshRequestDto refreshRequestDto);
}
