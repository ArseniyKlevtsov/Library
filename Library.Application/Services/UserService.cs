using Library.Application.DTOs.UserDtos.Request;
using Library.Application.DTOs.UserDtos.Response;
using Library.Application.Interfaces.Services;

namespace Library.Application.Services;

public class UserService : IUserService
{
    public Task CreateUserAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponseDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(int userId, UserRequestDto userRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
