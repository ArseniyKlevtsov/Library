﻿using AutoMapper;
using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.UserDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;

namespace Library.Application.UseCases.Auth;

public class Register: IRegisterUseCase
{
    private readonly IAccountManager _accountManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public Register(UnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
    {
        _accountManager = unitOfWork.AccountManager;
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

        var isUserCreated = await _accountManager.CreateAsync(user, registerRequestDto.Password!);
        if (isUserCreated == false)
        {
            throw new ArgumentException($"Failed to register user", new Exception());
        }

        await _accountManager.AddToRoleAsync(user, "User");
        var createdUser = await _accountManager.FindByNameAsync(user.UserName!);
        return _mapper.Map<UserResponseDto>(createdUser);
    }

    private async Task<bool> IsUserExsistAsync(string userName)
    {
        var existedUser = await _accountManager.FindByNameAsync(userName);
        if (existedUser == null)
        {
            return false;
        }
        return true;
    }
}
