using Library.Application.DTOs.GenreDtos.Response;

namespace Library.Application.Interfaces.UseCases.GenreUseCases;

public interface IGetGenreById
{
    Task<GenreResponseDto> ExecuteAsync(Guid id, CancellationToken cancellationToken);
}
