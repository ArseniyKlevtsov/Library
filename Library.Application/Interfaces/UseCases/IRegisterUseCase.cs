using Library.Application.DTOs.AuthDtos.Request;
using Library.Application.DTOs.UserDtos.Response;

namespace Library.Application.Interfaces.UseCases;

public interface IRegisterUseCase
{
    Task<UserResponseDto> ExecuteAsync(RegisterRequestDto registerRequestDto);
}
