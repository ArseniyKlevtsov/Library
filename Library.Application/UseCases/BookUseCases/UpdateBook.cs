using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces;
using System.Linq.Expressions;

namespace Library.Application.UseCases.BooksUseCases;

public class UpdateBook: IUpdateBook
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBook(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookResponseDto> ExecuteAsync(Guid id, BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(id, cancellationToken);
        if (book == null)
        {
            throw new KeyNotFoundException($"book with ID {id} not found.");
        }

        await ReBindGenres(book, bookRequestDto.GenreIds, cancellationToken);
        await UpdateInventoryForBook(book, bookRequestDto, cancellationToken);
        await UpdateBookImage(book, bookRequestDto, cancellationToken);

        _mapper.Map(bookRequestDto, book);
        await _unitOfWork.Books.UpdateAsync(book, cancellationToken);
        await _unitOfWork.SaveAsync();
        var updatedBook = _mapper.Map<BookResponseDto>(book);
        
        return updatedBook;
    }

    private async Task UpdateInventoryForBook(Book book,BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var inventory = await _unitOfWork.LibraryInventorys.GetByIdAsync(book.InventoryId, cancellationToken);
        _mapper.Map(bookRequestDto, inventory);
        await _unitOfWork.LibraryInventorys.UpdateAsync(inventory, cancellationToken);
    }

    private async Task ReBindGenres(Book book, ICollection<Guid>? genreIds, CancellationToken cancellationToken)
    {
        var includeState = new GenreIncludeState();

        if (genreIds == null)
        {
            return;
        }

        var ids = genreIds.ToList();

        Expression<Func<Genre, bool>> predicate = genre => ids.Contains(genre.Id);

        var genres = await _unitOfWork.Genres.GetWithIncludeByPredicateAsync(predicate, includeState, cancellationToken);

        book.Genres = genres.ToList();
    }

    private async Task UpdateBookImage(Book book, BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var bookImage = await _unitOfWork.BookImages.GetByIdAsync(book.BookImageId, cancellationToken);
        _mapper.Map(bookRequestDto, bookImage);
        await _unitOfWork.BookImages.UpdateAsync(bookImage!, cancellationToken);
    }
}
