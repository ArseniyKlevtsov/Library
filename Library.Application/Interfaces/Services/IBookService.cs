using Library.Application.DTOs.BookDtos.Response;
using Library.Application.DTOs.BookDtos.Request;

namespace Library.Application.Interfaces.Services;

public interface IBookService
{
    Task<BookResponseDto> GetBookByIdAsync(int bookId, CancellationToken cancellationToken);
    Task<IEnumerable<BookResponseDto>> GetAllBooksAsync(CancellationToken cancellationToken);

    Task CreateBookAsync(BookRequestDto bookRequestDto, CancellationToken cancellationToken);
    Task UpdateBookAsync(int bookId, BookRequestDto bookRequestDto, CancellationToken cancellationToken);
    Task DeleteBookAsync(int bookId, CancellationToken cancellationToken);
}
