using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;

namespace Library.Application.Interfaces.UseCases.Authors;

public interface IGetAuthorsPage
{
    Task<AuthorsResponseDto> ExecuteAsync(GetAllAuthorsRequestDto getAllAuthorsRequestDto, CancellationToken cancellationToken);
}
