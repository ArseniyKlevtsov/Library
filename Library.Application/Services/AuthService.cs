using AutoMapper;
using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.AuthDtos.Response;
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

    public AuthService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper, IConfiguration configuration, ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mapper = mapper;
        _tokenService = tokenService;
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

        return await _tokenService.GenerateTokensAsync(user);
    }

    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
    {
        //string accessToken, string refreshToken
        throw new NotImplementedException();
    }
}
