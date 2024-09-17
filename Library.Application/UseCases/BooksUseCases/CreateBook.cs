using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.Entities;
using Library.Infrastructure;

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
        await _unitOfWork.Books.AddAsync(book, cancellationToken);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<BookResponseDto>(book);
    }
}
