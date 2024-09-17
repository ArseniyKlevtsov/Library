using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;

namespace Library.Application.Interfaces.UseCases.BookUseCases;

public interface IUpdateBook
{
    Task<BookResponseDto> ExecuteAsync(Guid id, BookRequestDto bookRequestDto, CancellationToken cancellationToken);
}
