using AutoMapper;
using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.Auth;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Services;
using Library.Domain.Tokens;

namespace Library.Application.UseCases.Auth;

public class RefreshAuthorization : IRefreshAuthorizationUseCase
{
    private readonly IJwtTokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly string _loginProvider = "LibraryApp";
    private readonly string refreshTokenName = "RefreshToken";

    public RefreshAuthorization(IUnitOfWork unitOfWork, IJwtTokenService tokenService, IMapper mapper)
    {
        _tokenService = tokenService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TokenResponse> ExecuteAsync(RefreshRequestDto refreshRequestDto)
    {
        var user = await _tokenService.GetUserFromTokenAsync(refreshRequestDto.ExpiredAccessToken!);
        if (user == null)
        {
            throw new ReadTokenException("Authorization update denied");
        }

        var isValid = refreshRequestDto.RefreshToken == await _unitOfWork.AccountManager.GetAuthenticationTokenAsync(user, _loginProvider, refreshTokenName);
        if (isValid == false)
        {
            throw new ReadTokenException("Authorization update denied");
        }

        var newToken = await _tokenService.GenerateTokensAsync(user);
        await _tokenService.SetRefreshTokenAsync(user, newToken.RefreshToken!);
        return newToken;
    }
}
