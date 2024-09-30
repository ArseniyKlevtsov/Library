using AutoMapper;
using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.GenresUseCases;

public class UpdateGenre : IUpdateGenre
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateGenre(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<GenreResponseDto> ExecuteAsync(Guid id, GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(id, cancellationToken);
        if (genre == null)
        {
            throw new KeyNotFoundException($"Genre with ID {id} not found.");
        }
        _mapper.Map(genreRequestDto, genre);
        await _unitOfWork.Genres.UpdateAsync(genre, cancellationToken);
        await _unitOfWork.SaveAsync();
        return _mapper.Map<GenreResponseDto>(genre);
    }
}
