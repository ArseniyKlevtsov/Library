using AutoMapper;
using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.AuthDtos.Response;
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
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
    [Authorize]
    public async Task<ActionResult<TokenResponse>> RefreshTokenAsync()
    {
       throw new NotImplementedException();
    }
}