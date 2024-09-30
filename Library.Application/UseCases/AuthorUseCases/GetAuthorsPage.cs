using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Interfaces;
using Library.Domain.SearchCriteries;

namespace Library.Application.UseCases.AuthorsUseCases;

public class GetAuthorsPage : IGetAuthorsPage
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorsPage(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<AuthorsResponseDto> ExecuteAsync(GetAllAuthorsRequestDto getAllAuthorsRequestDto, CancellationToken cancellationToken)
    {
        var criterias = _mapper.Map<AuthorCriterias>(getAllAuthorsRequestDto);
        var authors = await _unitOfWork.Authors.GetAuthorsWithCriterias(criterias ,cancellationToken);
        var totalCount = await _unitOfWork.Authors.GetCountAsync(cancellationToken);

        var authorsResponse = new AuthorsResponseDto()
        {
            TotalCount = totalCount,
            TotalPages = GetPagesCount(getAllAuthorsRequestDto, totalCount),
            Authors = _mapper.Map<IEnumerable<AuthorResponseDto>>(authors)
        };

        return authorsResponse;
    }

    private int GetPagesCount(GetAllAuthorsRequestDto getAllAuthorsRequestDto, int totalCount)
    {
        var hasValue = getAllAuthorsRequestDto.PageSize.HasValue;
        if (hasValue)
        {
            return (int)Math.Ceiling((double)totalCount / (int)getAllAuthorsRequestDto.PageSize!);
        }

        return 0;
    }

}
