using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;

namespace Library.Application.Interfaces.Services;

public interface IAuthorService
{
    Task<AuthorResponseDto> GetAuthorByIdAsync(Guid authorId, CancellationToken cancellationToken);
    Task<AuthorsResponseDto> GetAllAuthorsAsync(GetAllAuthorsRequestDto getAllAuthorsRequestDto, CancellationToken cancellationToken);

    Task CreateAuthorAsync(AuthorRequestDto authorRequestDto, CancellationToken cancellationToken);
    Task UpdateAuthorAsync(Guid authorId, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken);
    Task DeleteAuthorAsync(Guid authorId, CancellationToken cancellationToken);
}
