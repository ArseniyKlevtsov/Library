using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.Services;

namespace Library.Application.Services;

public class AuthorService : IAuthorService
{
    public Task CreateAuthorAsync(AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAuthorAsync(int authorId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AuthorResponseDto>> GetAllAuthorsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AuthorResponseDto> GetAuthorByIdAsync(int authorId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAuthorAsync(int authorId, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
