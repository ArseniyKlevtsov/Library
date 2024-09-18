using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.UseCases.GenreUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly ICreateGenre _createGenre;
    private readonly IGetGenreById _getGenreById;
    private readonly IDeleteGenre _deleteGenre;
    private readonly IUpdateGenre _updateGenre;
    private readonly IGetGenresPage _getGenresPage;

    public GenresController(ICreateGenre createGenre, IGetGenreById getGenreById, IDeleteGenre deleteGenre, IUpdateGenre updateGenre, IGetGenresPage getGenresPage)
    {
        _createGenre = createGenre;
        _getGenreById = getGenreById;
        _deleteGenre = deleteGenre;
        _updateGenre = updateGenre;
        _getGenresPage = getGenresPage;
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<GenreResponseDto>> GetGenreById(Guid id, CancellationToken cancellationToken)
    {
        var genre = await _getGenreById.ExecuteAsync(id, cancellationToken);
        return Ok(genre);
    }

    [HttpPost("getAll")]
    //[Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<GenresResponseDto>> GetAllGenres(
        GetAllGenresRequestDto getAllGenresRequestDto,
        CancellationToken cancellationToken = default)
    {
        var genres = await _getGenresPage.ExecuteAsync(getAllGenresRequestDto, cancellationToken);
        return Ok(genres);
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateGenre(GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        var createdGenre = await _createGenre.ExecuteAsync(genreRequestDto, cancellationToken);
        return Ok(createdGenre);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateGenre(Guid id, GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        var updatedGenre = await _updateGenre.ExecuteAsync(id, genreRequestDto, cancellationToken);
        return Ok(updatedGenre);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteGenre(Guid id, CancellationToken cancellationToken)
    {
        await _deleteGenre.ExecuteAsync(id, cancellationToken);
        return Ok();
    }
}