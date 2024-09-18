using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;

namespace Library.Application.Interfaces.UseCases.GenreUseCases;

public interface IGetGenresPage
{
    Task<GenresResponseDto> ExecuteAsync(GetAllGenresRequestDto getAllGenresRequestDto ,CancellationToken cancellationToken);
}
