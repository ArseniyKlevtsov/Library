using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Infrastructure;
using System.Linq.Expressions;

namespace Library.Application.UseCases.BooksUseCases;

public class UpdateBook: IUpdateBook
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBook(UnitOfWork unitOfWork, IMapper mapper)
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

        _mapper.Map(bookRequestDto, book);
        await _unitOfWork.Books.UpdateAsync(book, cancellationToken);
        await _unitOfWork.SaveAsync();
        var updatedBook = _mapper.Map<BookResponseDto>(book);
        return updatedBook;
    }

    private async Task<Guid> UpdateInventoryForBook(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task ReBindGenres(Book book, ICollection<Guid>? genreIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task<Guid> UpdateBookImage(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
