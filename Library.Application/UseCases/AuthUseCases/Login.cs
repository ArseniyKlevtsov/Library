using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.AuthDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;
using System.Security.Authentication;

namespace Library.Application.UseCases.Auth;

public class Login : ILoginUseCase
{
    private readonly IAccountManager _accountManager;
    private readonly ITokenService _tokenService;

    public Login(UnitOfWork unitOfWork, ITokenService tokenService)
    {
        _accountManager = unitOfWork.AccountManager;
        _tokenService = tokenService;
    }

    public async Task<TokenResponse> ExecuteAsync(LoginRequestDto loginRequestDto)
    {
        var user = await _accountManager.FindByNameAsync(loginRequestDto.UserName!);

        var userAuthenticated = user != null && await _accountManager.CheckPasswordAsync(user, loginRequestDto.Password!);
        if (userAuthenticated == false)
        {
            throw new AuthenticationException($"user {loginRequestDto.UserName} was not found or the password is incorrect");
        }

        var token = await _tokenService.GenerateTokensAsync(user!);
        await _tokenService.SetRefreshTokenAsync(user!, token.RefreshToken!);
        
        return token;
    }
}
