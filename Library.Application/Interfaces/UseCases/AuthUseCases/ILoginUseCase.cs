using Library.Application.DTOs.AuthDtos.Request;
using Library.Domain.Tokens;

namespace Library.Application.Interfaces.UseCases.Auth;

public interface ILoginUseCase
{
    Task<TokenResponse> ExecuteAsync(LoginRequestDto loginRequestDto);
}
