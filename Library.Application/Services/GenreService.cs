using AutoMapper;
using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure;

namespace Library.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _genreRepository = unitOfWork.Genres;
        _mapper = mapper;
    }

    public async Task<GenreResponseDto> GetGenreByIdAsync(Guid genreId, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(genreId, cancellationToken);
        if (genre == null)
        {
            throw new KeyNotFoundException($"Genre with ID {genreId} not found.");
        }

        var genreResponseDto = _mapper.Map<GenreResponseDto>(genre);
        return genreResponseDto;
    }

    public async Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var genres = await _genreRepository.GetAllAsync(cancellationToken);

        var totalCount = genres.Count();
        var skipCount = (pageNumber - 1) * pageSize;
        var pagedGenres = genres.Skip(skipCount).Take(pageSize);

        var genreResponseDtos = _mapper.Map<IEnumerable<GenreResponseDto>>(pagedGenres);
        return genreResponseDtos;
    }

    public async Task CreateGenreAsync(GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        var genre = _mapper.Map<Genre>(genreRequestDto);
        await _genreRepository.AddAsync(genre, cancellationToken);
    }

    public async Task UpdateGenreAsync(Guid genreId, GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(genreId, cancellationToken);
        if (genre == null)
        {
            throw new KeyNotFoundException($"Genre with ID {genreId} not found.");
        }

        _mapper.Map(genreRequestDto, genre);
        await _genreRepository.UpdateAsync(genre, cancellationToken);
    }

    public async Task DeleteGenreAsync(Guid genreId, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(genreId, cancellationToken);
        if (genre == null)
        {
            throw new KeyNotFoundException($"Genre with ID {genreId} not found.");
        }

        await _genreRepository.DeleteAsync(genre, cancellationToken);
    }
}