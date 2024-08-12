using Library.Application.DTOs.BookDtos.Response;
using Library.Application.DTOs.BookDtos.Request;

namespace Library.Application.Interfaces.Services;

public interface IBookService
{
    Task<BookResponseDto> GetBookByIdAsync(Guid bookId, CancellationToken cancellationToken);
    Task<IEnumerable<BookResponseDto>> GetAllBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task CreateBookAsync(BookRequestDto bookRequestDto, CancellationToken cancellationToken);
    Task UpdateBookAsync(Guid bookId, BookRequestDto bookRequestDto, CancellationToken cancellationToken);
    Task DeleteBookAsync(Guid bookId, CancellationToken cancellationToken);
}
