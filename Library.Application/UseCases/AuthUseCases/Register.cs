using AutoMapper;
using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.UserDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.Auth;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Services;

namespace Library.Application.UseCases.Auth;

public class Register: IRegisterUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _tokenService;
    private readonly IMapper _mapper;

    public Register(IUnitOfWork unitOfWork, IJwtTokenService tokenService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> ExecuteAsync(RegisterRequestDto registerRequestDto)
    {
        var user = _mapper.Map<User>(registerRequestDto);

        var isUserExist = await IsUserExsistAsync(user.UserName!);
        if (isUserExist)
        {
            throw new AlreadyExistsException($"User with this username:{user.UserName!} already exists");
        }

        var isUserCreated = await _unitOfWork.AccountManager.CreateAsync(user, registerRequestDto.Password!);
        if (isUserCreated == false)
        {
            throw new NotFoundException($"Failed to register user");
        }

        await _unitOfWork.AccountManager.AddToRoleAsync(user, "User");
        var createdUser = await _unitOfWork.AccountManager.FindByNameAsync(user.UserName!);
        return _mapper.Map<UserResponseDto>(createdUser);
    }

    private async Task<bool> IsUserExsistAsync(string userName)
    {
        var existedUser = await _unitOfWork.AccountManager.FindByNameAsync(userName);
        if (existedUser == null)
        {
            return false;
        }
        return true;
    }
}
