using AutoMapper;
using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.UseCases.BookUseCases;
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
        var authors = await _unitOfWork.Books.GetBooksWithCriterias(criterias, cancellationToken);
        var totalCount = await _unitOfWork.Books.GetCountAsync(cancellationToken);

        var booksResponse = new BooksResponseDto()
        {
            TotalCount = totalCount,
            TotalPages = GetPagesCount(getAllBooksRequestDto, totalCount),
            Books = _mapper.Map<IEnumerable<BookResponseDto>>(authors)
        };

        return booksResponse;
    }

    private int GetPagesCount(GetAllBooksRequestDto getAllBooksRequestDto, int totalCount)
    {
        var hasValue = getAllBooksRequestDto.PageSize.HasValue;
        if (hasValue)
        {
            return totalCount / (int)getAllBooksRequestDto.PageSize!;
        }

        return 0;
    }
}
