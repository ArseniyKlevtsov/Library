using AutoMapper;
using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.GenresUseCases;

public class CreateGenre : ICreateGenre
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGenre(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<GenreResponseDto> ExecuteAsync(GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        var genre = _mapper.Map<Genre>(genreRequestDto);
        await _unitOfWork.Genres.AddAsync(genre, cancellationToken);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<GenreResponseDto>(genre);
    }
}
