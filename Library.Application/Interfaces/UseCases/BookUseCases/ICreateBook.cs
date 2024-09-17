using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;

namespace Library.Application.Interfaces.UseCases.BookUseCases;

public interface ICreateBook
{
    Task<BookResponseDto> ExecuteAsync(BookRequestDto bookRequestDto, CancellationToken cancellationToken);
}
