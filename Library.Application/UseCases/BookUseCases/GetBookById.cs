﻿using AutoMapper;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.BooksUseCases;

public class GetBookById: IGetBookById
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookById(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var includeState = new BookIncludeState()
        {
            IncludeBookImage = true,
            IncludeGenres = true,
            IncludeInventory = true,
            IncludeRentedBooks = true,
        };

        var books = await _unitOfWork.Books.GetWithIncludeByPredicateAsync(book => book.Id == id, includeState, cancellationToken);
        var book = books.FirstOrDefault();
        if (book == null)
        {
            throw new NotFoundException($"Book with ID {id} not found.");
        }

        var bookResponseDto = _mapper.Map<BookResponseDto>(book);
        return bookResponseDto;
    }
}
