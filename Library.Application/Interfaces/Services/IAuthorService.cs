using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;

namespace Library.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<AuthorDto> GetAuthorByIdAsync(int authorId, CancellationToken cancellationToken);
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken);

    Task CreateAuthorAsync(AuthorRequestDto productRequestDto, CancellationToken cancellationToken);
    Task UpdateAuthorAsync(int authorId, AuthorRequestDto productRequestDto, CancellationToken cancellationToken);
    Task DeleteAuthorAsync(int authorId, CancellationToken cancellationToken);
}
