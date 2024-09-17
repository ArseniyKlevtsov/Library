using Library.Application.DTOs.AuthorDtos.Response;

namespace Library.Application.Interfaces.UseCases.Authors;

public interface IGetAuthorById
{
    Task<AuthorResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}
