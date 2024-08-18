using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;
using static System.Reflection.Metadata.BlobBuilder;

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

    public async Task<BooksResponseDto> GetAllBooksAsync(GetAllBooksRequestDto getAllBooksRequestDto, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(cancellationToken);

        // filtration
        if (!string.IsNullOrEmpty(getAllBooksRequestDto.BookNameFilter))
        {
            var bookNameFilter = getAllBooksRequestDto.BookNameFilter.ToLower();
            books = books.Where(b => b.Name!.ToLower().Contains(bookNameFilter));
        }
        if (getAllBooksRequestDto.AuthorId.HasValue)
        {
            var authorId = getAllBooksRequestDto.AuthorId.Value;
            books = books.Where(b => b.AuthorId == authorId);
        }
        if (getAllBooksRequestDto.GenreId.HasValue)
        {
            var genreId = getAllBooksRequestDto.GenreId.Value;
            books = books.Where(b => b.Genres!.Any(g => g.Id == genreId));
        }
        if (getAllBooksRequestDto.RentOrderId.HasValue)
        {
            var rentOrderId = getAllBooksRequestDto.RentOrderId.Value;
            books = books.Where(b => b.RentedBooks!.Any(r => r.RentOrderId == rentOrderId));
        }

        // sotring
        if (getAllBooksRequestDto.BookNameSortAsc.HasValue)
        {
            books = getAllBooksRequestDto.BookNameSortAsc.Value
                ? books.OrderBy(b => b.Name)
                : books.OrderByDescending(b => b.Name);
        }
        if (getAllBooksRequestDto.IsbnSortAsc.HasValue)
        {
            books = getAllBooksRequestDto.IsbnSortAsc.Value
                ? books.OrderBy(b => b.Isbn)
                : books.OrderByDescending(b => b.Isbn);
        }

        // pagination
        var totalCount = books.Count();
        var pageNumber = getAllBooksRequestDto.Page;
        var pageSize = getAllBooksRequestDto.PageSize;
        var skipCount = (pageNumber - 1) * pageSize;
        var pagedBooks = books.Skip(skipCount).Take(pageSize);

        var bookResponseDtos = _mapper.Map<IEnumerable<BookResponseDto>>(pagedBooks);

        return new BooksResponseDto
        {
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
            Books = bookResponseDtos
        };
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