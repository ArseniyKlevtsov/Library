using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _bookRepository = unitOfWork.Books;
        _mapper = mapper;
    }

    public async Task<BookResponseDto> GetBookByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {bookId} not found.");
        }

        var bookResponseDto = _mapper.Map<BookResponseDto>(book);
        return bookResponseDto;
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(cancellationToken);

        var totalCount = books.Count();
        var skipCount = (pageNumber - 1) * pageSize;
        var pagedBooks = books.Skip(skipCount).Take(pageSize);

        var bookResponseDtos = _mapper.Map<IEnumerable<BookResponseDto>>(pagedBooks);
        return bookResponseDtos;
    }

    public async Task CreateBookAsync(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(bookRequestDto);
        await _bookRepository.AddAsync(book, cancellationToken);
    }

    public async Task UpdateBookAsync(Guid bookId, BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {bookId} not found.");
        }

        _mapper.Map(bookRequestDto, book);
        await _bookRepository.UpdateAsync(book, cancellationToken);
    }

    public async Task DeleteBookAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {bookId} not found.");
        }

        await _bookRepository.DeleteAsync(book, cancellationToken);
    }
}