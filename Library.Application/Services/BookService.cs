using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.Services;

namespace Library.Application.Services;

public class BookService : IBookService
{
    public Task CreateBookAsync(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookAsync(int bookId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BookResponseDto>> GetAllBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<BookResponseDto> GetBookByIdAsync(int bookId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBookAsync(int bookId, BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
