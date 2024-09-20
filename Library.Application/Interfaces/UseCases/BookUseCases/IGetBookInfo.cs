using Library.Application.DTOs.BookDtos.Response;

namespace Library.Application.Interfaces.UseCases.BookUseCases;

public interface IGetBookInfo
{
    Task<BookInfoResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}
