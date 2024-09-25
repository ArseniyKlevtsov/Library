using AutoMapper;
using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.Auth;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Interfaces.Services;
using Library.Domain.Tokens;
using Library.Infrastructure;

namespace Library.Application.UseCases.Auth;

public class RefreshAuthorization : IRefreshAuthorizationUseCase
{
    private readonly IAccountManager _accountManager;
    private readonly IJwtTokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly string _loginProvider = "LibraryApp";
    private readonly string refreshTokenName = "RefreshToken";

    public RefreshAuthorization(UnitOfWork unitOfWork, IJwtTokenService tokenService, IMapper mapper)
    {
        _accountManager = unitOfWork.AccountManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<TokenResponse> ExecuteAsync(RefreshRequestDto refreshRequestDto)
    {
        var user = await _tokenService.GetUserFromTokenAsync(refreshRequestDto.ExpiredAccessToken!);
        if (user == null)
        {
            throw new ReadTokenException("Authorization update denied");
        }

        var isValid = refreshRequestDto.RefreshToken == await _accountManager.GetAuthenticationTokenAsync(user, _loginProvider, refreshTokenName);
        if (isValid == false)
        {
            throw new ReadTokenException("Authorization update denied");
        }

        var newToken = await _tokenService.GenerateTokensAsync(user);
        await _tokenService.SetRefreshTokenAsync(user, newToken.RefreshToken!);
        return newToken;
    }
}
