using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;

namespace Library.Application.Interfaces.UseCases.GenreUseCases;

public interface ICreateGenre
{
    Task<GenreResponseDto> ExecuteAsync(GenreRequestDto genreRequestDto, CancellationToken cancellationToken);
}
