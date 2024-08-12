using Library.Application.DTOs.UserDtos.Request;
using Library.Application.DTOs.UserDtos.Response;
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<UserResponseDto>> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);
        return Ok(user);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetAllUsersAsync(pageNumber, pageSize, cancellationToken);
        return Ok(users);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateUser(UserRequestDto userRequestDto, CancellationToken cancellationToken)
    {
        await _userService.CreateUserAsync(userRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateUser(Guid id, UserRequestDto userRequestDto, CancellationToken cancellationToken)
    {
        await _userService.UpdateUserAsync(id, userRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        await _userService.DeleteUserAsync(id, cancellationToken);
        return NoContent();
    }
}