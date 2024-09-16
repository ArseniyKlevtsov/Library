using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.UserDtos.Response;

namespace Library.Application.Interfaces.UseCases.Auth;

public interface IRegisterUseCase
{
    Task<UserResponseDto> ExecuteAsync(RegisterRequestDto registerRequestDto);
}
