using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.Interfaces.UseCases.Auth;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Services;
using Library.Domain.Tokens;
using System.Security.Authentication;

namespace Library.Application.UseCases.Auth;

public class Login : ILoginUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _tokenService;

    public Login(IUnitOfWork unitOfWork, IJwtTokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<TokenResponse> ExecuteAsync(LoginRequestDto loginRequestDto)
    {
        var user = await _unitOfWork.AccountManager.FindByNameAsync(loginRequestDto.UserName!);

        var userAuthenticated = user != null && await _unitOfWork.AccountManager.CheckPasswordAsync(user, loginRequestDto.Password!);
        if (userAuthenticated == false)
        {
            throw new AuthenticationException($"user {loginRequestDto.UserName} was not found or the password is incorrect");
        }

        var token = await _tokenService.GenerateTokensAsync(user!);
        await _tokenService.SetRefreshTokenAsync(user!, token.RefreshToken!);
        
        return token;
    }
}
