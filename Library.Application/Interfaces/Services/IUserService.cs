using Library.Application.DTOs.UserDtos.Request;
using Library.Application.DTOs.UserDtos.Response;

namespace Library.Application.Interfaces.Services;

public interface IUserService
{
    Task<UserResponseDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task CreateUserAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken);
    Task UpdateUserAsync(Guid userId, UserRequestDto userRequestDto, CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
}
