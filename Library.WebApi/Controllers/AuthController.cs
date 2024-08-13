using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.AuthDtos.Response;
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        await _authService.RegisterAsync(registerRequestDto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var tokenResponse = await _authService.LoginAsync(loginRequestDto);
        return Ok(tokenResponse);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenResponse>> RefreshTokenAsync(RefreshRequestDto refreshRequestDto)
    {
        var tokenResponse = await _authService.RefreshTokenAsync(refreshRequestDto);
        return Ok(tokenResponse);
    }
}