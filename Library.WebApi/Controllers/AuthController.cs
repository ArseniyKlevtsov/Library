using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.AuthDtos.Response;
using Library.Application.DTOs.UserDtos.Response;
using Library.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _login;
    private readonly IRegisterUseCase _register;
    private readonly IRefreshAuthorizationUseCase _refresh;

    public AuthController (ILoginUseCase login, IRegisterUseCase register, IRefreshAuthorizationUseCase refrehs)
    {
        _login = login;
        _register = register;
        _refresh = refrehs;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        var userResponseDto = await _register.ExecuteAsync(registerRequestDto);
        return Ok(userResponseDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var tokenResponse = await _login.ExecuteAsync(loginRequestDto);
        return Ok(tokenResponse);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenResponse>> RefreshAsync(RefreshRequestDto refreshRequestDto)
    {
        var tokenResponse = await _refresh.ExecuteAsync(refreshRequestDto);
        return Ok(tokenResponse);
    }
}