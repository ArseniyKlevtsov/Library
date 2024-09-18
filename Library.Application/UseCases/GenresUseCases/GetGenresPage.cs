using AutoMapper;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Library.Domain.SearchCriterias;
using Library.Infrastructure;

namespace Library.Application.UseCases.GenresUseCases;

public class GetGenresPage: IGetGenresPage
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGenresPage(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GenresResponseDto> ExecuteAsync(GetAllGenresRequestDto getAllGenresRequestDto, CancellationToken cancellationToken)
    {
        var criteries = _mapper.Map<GenreCriterias>(getAllGenresRequestDto);
        var genres = await _unitOfWork.Genres.GetGenresWithCriterias(criteries, cancellationToken);
        var totalCount = await _unitOfWork.Genres.GetCountAsync(cancellationToken);

        var genresResponse = new GenresResponseDto()
        {
            TotalCount = totalCount,
            TotalPages = GetPagesCount(getAllGenresRequestDto, totalCount),
            Genres = _mapper.Map<IEnumerable<GenreResponseDto>>(genres)
        };

        return genresResponse;
    }
    private int GetPagesCount(GetAllGenresRequestDto getAllGenresRequestDto, int totalCount)
    {
        var hasValue = getAllGenresRequestDto.PageSize.HasValue;
        if (hasValue)
        {
            return totalCount / (int)getAllGenresRequestDto.PageSize!;
        }

        return 0;
    }

}
