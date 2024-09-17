using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;

namespace Library.Application.Interfaces.UseCases.Authors;

public interface IUpdateAuthor
{
    Task<AuthorResponseDto> ExecuteAsync(Guid authorId, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken);
}
