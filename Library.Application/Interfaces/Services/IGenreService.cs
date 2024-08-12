﻿using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;

namespace Library.Application.Interfaces.Services;

public interface IGenreService
{
    Task<GenreResponseDto> GetGenreByIdAsync(Guid genreId, CancellationToken cancellationToken);
    Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task CreateGenreAsync(GenreRequestDto genreRequestDto, CancellationToken cancellationToken);
    Task UpdateGenreAsync(Guid genreId, GenreRequestDto genreRequestDto, CancellationToken cancellationToken);
    Task DeleteGenreAsync(Guid genreId, CancellationToken cancellationToken);
}
