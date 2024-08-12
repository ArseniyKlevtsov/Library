using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.Services;

namespace Library.Application.Services;

public class GenreService : IGenreService
{
    public Task CreateGenreAsync(GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteGenreAsync(int genreId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GenreResponseDto> GetGenreByIdAsync(int genreId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateGenreAsync(int genreId, GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
