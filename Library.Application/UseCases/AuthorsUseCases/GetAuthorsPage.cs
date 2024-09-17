using AutoMapper;
using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.UseCases.Authors;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.SearchCriteries;
using Library.Infrastructure;

namespace Library.Application.UseCases.AuthorsUseCases;

public class GetAuthorsPage : IGetAuthorsPage
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public GetAuthorsPage(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authorRepository = unitOfWork.Authors;
        _mapper = mapper;
    }
    public async Task<AuthorsResponseDto> ExecuteAsync(GetAllAuthorsRequestDto getAllAuthorsRequestDto, CancellationToken cancellationToken)
    {
        var criterias = _mapper.Map<AuthorCriterias>(getAllAuthorsRequestDto);
        var authors = await _authorRepository.GetAuthorsWithCriterias(criterias ,cancellationToken);
        var totalCount = await _authorRepository.GetCountAsync(cancellationToken);

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
            return totalCount / (int)getAllAuthorsRequestDto.PageSize!;
        }

        return 0;
    }

}
