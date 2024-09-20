using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Exceptions;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.IncludeStates;
using Library.Infrastructure;

namespace Library.Application.UseCases.BookUseCases;

public class GetBookInfo: IGetBookInfo
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookInfo(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookInfoResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var includeState = new BookIncludeState()
        {
            IncludeBookImage = true,
            IncludeGenres = true,
            IncludeInventory = true,
            IncludeAuthors = true,
        };

        var books = await _unitOfWork.Books.GetWithIncludeByPredicateAsync(book => book.Id == id, includeState, cancellationToken);
        var book = books.FirstOrDefault();
        if (book == null)
        {
            throw new NotFoundException($"Book with ID {id} not found.");
        }

        var bookResponseDto = _mapper.Map<BookInfoResponseDto>(book);
        bookResponseDto.Author = _mapper.Map<AuthorResponseDto>(book.Author);
        bookResponseDto.Genres = _mapper.Map<IEnumerable<GenreResponseDto>>(book.Genres);

        return bookResponseDto;
    }
}
