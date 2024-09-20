using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Library.Domain.IncludeStates;
using Library.Domain.SearchCriterias;
using Library.Infrastructure;

namespace Library.Application.UseCases.BooksUseCases;

public class GetBooksPage: IGetBooksPage
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBooksPage(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BooksResponseDto> ExecuteAsync(GetAllBooksRequestDto getAllBooksRequestDto, CancellationToken cancellationToken)
    {
        var criterias = _mapper.Map<BookCriterieas>(getAllBooksRequestDto);
        var includeState = new BookIncludeState()
        {
            IncludeBookImage = true,
            IncludeInventory = true,
        };
        var books = await _unitOfWork.Books.GetBooksWithCriterias(criterias, includeState, cancellationToken);
        var totalCount = await _unitOfWork.Books.GetCountAsync(cancellationToken);

        var booksResponse = new BooksResponseDto()
        {
            TotalCount = totalCount,
            TotalPages = GetPagesCount(getAllBooksRequestDto, totalCount),
            Books = _mapper.Map<IEnumerable<BookPreviewResponseDto>>(books)
        };

        return booksResponse;
    }

    private int GetPagesCount(GetAllBooksRequestDto getAllBooksRequestDto, int totalCount)
    {
        var hasValue = getAllBooksRequestDto.PageSize.HasValue;
        if (hasValue)
        {
            return (int)Math.Ceiling((double)totalCount / (int)getAllBooksRequestDto.PageSize!);
        }

        return 0;
    }
}
