using AutoMapper;
using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.AuthDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Authentication;

namespace Library.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly string _loginProvider;
    private readonly string refreshTokenName = "RefreshToken";

    public AuthService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper, IConfiguration configuration, ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mapper = mapper;
        _tokenService = tokenService;
        _loginProvider = _configuration["App:Name"]!;
    }

    public async Task RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        var user = _mapper.Map<User>(registerRequestDto);

        var result = await _userManager.CreateAsync(user, registerRequestDto.Password!);


        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }
        else
        {
            var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ArgumentException($"Failed to register user: {errorMessage}", new Exception());
        }
    }

    public async Task<TokenResponse> LoginAsync(LoginRequestDto loginRequestDto)
    {
        if (loginRequestDto.UserName== null)
        {
            throw new ArgumentException($"UserName cannot be null", new Exception());
        }
        var user = await _userManager.FindByNameAsync(loginRequestDto.UserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequestDto.Password!))
        {
            throw new AuthenticationException($"user {loginRequestDto.UserName} was not found or the password is incorrect");
        }

        var token = await _tokenService.GenerateTokensAsync(user);
        await SetRefreshTokenAsync(user, token.RefreshToken!);

        return token;
    }

    public async Task<TokenResponse> RefreshTokenAsync(RefreshRequestDto refreshRequestDto)
    {

        var user = await _tokenService.GetUserFromTokenAsync(refreshRequestDto.ExpiredAccessToken!);
        if (user == null)
        {
            throw new ReadTokenException("The token was refused");
        }
        var isValid = (refreshRequestDto.RefreshToken == await _userManager.GetAuthenticationTokenAsync(user, _loginProvider, refreshTokenName));
        if (isValid==false) 
        {
            throw new ReadTokenException("The token was refused");
        }

        var newToken = await _tokenService.GenerateTokensAsync(user);
        await SetRefreshTokenAsync(user, newToken.RefreshToken!);
        return newToken;
    }

    private async Task SetRefreshTokenAsync(User user, string refreshToken)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user, _loginProvider, refreshTokenName);
        await _userManager.SetAuthenticationTokenAsync(user, _loginProvider, refreshTokenName, refreshToken);
    }
}
