using Library.Application.DTOs.BookDtos.Response;

namespace Library.Application.Interfaces.UseCases.BookUseCases;

public interface IGetBookById
{
    Task<BookResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}

