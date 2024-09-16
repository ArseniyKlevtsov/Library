using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;

namespace Library.Application.Interfaces.UseCases.Authors;

public interface ICreateAuthor
{
    Task<AuthorResponseDto> ExecuteAsync(AuthorRequestDto authorRequestDto, CancellationToken cancellationToken);
}
