using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;

namespace Library.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<AuthorResponseDto> GetAuthorByIdAsync(int authorId, CancellationToken cancellationToken);
    Task<IEnumerable<AuthorResponseDto>> GetAllAuthorsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task CreateAuthorAsync(AuthorRequestDto authorRequestDto, CancellationToken cancellationToken);
    Task UpdateAuthorAsync(int authorId, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken);
    Task DeleteAuthorAsync(int authorId, CancellationToken cancellationToken);
}
