using AutoMapper;
using Library.Application.DTOs.UserDtos.Request;
using Library.Application.DTOs.UserDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;

namespace Library.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = unitOfWork.Users;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        var userResponseDto = _mapper.Map<UserResponseDto>(user);
        return userResponseDto;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        var totalCount = users.Count();
        var skipCount = (pageNumber - 1) * pageSize;
        var pagedUsers = users.Skip(skipCount).Take(pageSize);

        var userResponseDtos = _mapper.Map<IEnumerable<UserResponseDto>>(pagedUsers);
        return userResponseDtos;
    }

    public async Task CreateUserAsync(UserRequestDto userRequestDto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Entities.User>(userRequestDto);
        await _userRepository.AddAsync(user, cancellationToken);
    }

    public async Task UpdateUserAsync(Guid userId, UserRequestDto userRequestDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        _mapper.Map(userRequestDto, user);
        await _userRepository.UpdateAsync(user, cancellationToken);
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        await _userRepository.DeleteAsync(user, cancellationToken);
    }
}