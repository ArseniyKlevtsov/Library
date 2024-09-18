using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;

namespace Library.Application.Interfaces.UseCases.GenreUseCases;

public interface IUpdateGenre
{
    Task<GenreResponseDto> ExecuteAsync(Guid id, GenreRequestDto genreRequestDto, CancellationToken cancellationToken);
}
