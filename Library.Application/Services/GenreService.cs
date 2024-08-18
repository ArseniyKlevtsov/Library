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

    public async Task<GenresResponseDto> GetAllGenresAsync(GetAllGenresRequestDto getAllGenresRequestDto, CancellationToken cancellationToken)
    {
        var genres = await _genreRepository.GetAllAsync(cancellationToken);

        // filtration
        if (getAllGenresRequestDto.BookId.HasValue)
        {
            var bookIdFilter = getAllGenresRequestDto.BookId.Value;
            genres = genres.Where(g => g.Books!.Any(b => b.Id == bookIdFilter)).ToList();
        }
        if (!string.IsNullOrEmpty(getAllGenresRequestDto.NameFilter))
        {
            var nameFilter = getAllGenresRequestDto.NameFilter.ToLower();
            genres = genres.Where(g => g.Name!.ToLower().Contains(nameFilter)).ToList();
        }

        // sorting
        if (getAllGenresRequestDto.NameSortAsc.HasValue)
        {
            if (getAllGenresRequestDto.NameSortAsc.Value)
            {
                genres = genres.OrderBy(g => g.Name).ToList();
            }
            else
            {
                genres = genres.OrderByDescending(g => g.Name).ToList();
            }
        }

        // pagination
        var totalCount = genres.Count();
        var skipCount = (getAllGenresRequestDto.Page - 1) * getAllGenresRequestDto.PageSize;
        var pagedGenres = genres.Skip(skipCount).Take(getAllGenresRequestDto.PageSize);

        var genreResponseDtos = _mapper.Map<IEnumerable<GenreResponseDto>>(pagedGenres);
        return new GenresResponseDto
        {
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / getAllGenresRequestDto.PageSize),
            Genres = genreResponseDtos
        };
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