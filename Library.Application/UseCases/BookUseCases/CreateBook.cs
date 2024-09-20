using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Infrastructure;
using System.Linq.Expressions;

namespace Library.Application.UseCases.BooksUseCases;

public class CreateBook : ICreateBook
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBook (UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookResponseDto> ExecuteAsync(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Book>(bookRequestDto);

        await BindGenres(book, bookRequestDto.GenreIds, cancellationToken);
        var bookImageId = await CreateBookImage(bookRequestDto, cancellationToken);
        var inventoryId = await CreateInventoryForBook(bookRequestDto, cancellationToken);

        book.BookImageId = bookImageId;
        book.InventoryId = inventoryId;

        await _unitOfWork.Books.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<BookResponseDto>(book);
    }

    private async Task<Guid> CreateInventoryForBook(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var inventory = _mapper.Map<LibraryInventory>(bookRequestDto);
        await _unitOfWork.LibraryInventorys.AddAsync(inventory, cancellationToken);
        return inventory.Id;
    }

    private async Task BindGenres(Book book, ICollection<Guid>? genreIds, CancellationToken cancellationToken)
    {
        var includeState = new GenreIncludeState();
        
        if (genreIds == null) 
        {
            return;
        }
        
        var ids = genreIds.ToList();
        
        Expression<Func<Genre, bool>> predicate = genre => ids.Contains(genre.Id);
        
        var genres = await _unitOfWork.Genres.GetWithIncludeByPredicateAsync(predicate, includeState, cancellationToken, true);
        
        book.Genres = genres.ToList();
    }

    private async Task<Guid> CreateBookImage(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var bookImage = _mapper.Map<BookImage>(bookRequestDto);
        await _unitOfWork.BookImages.AddAsync(bookImage, cancellationToken);
        return bookImage.Id;
    }
}
