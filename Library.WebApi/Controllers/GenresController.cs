using Library.Application.DTOs.GenreDtos.Request;
using Library.Application.DTOs.GenreDtos.Response;
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<GenreResponseDto>> GetGenreById(Guid id, CancellationToken cancellationToken)
    {
        var genre = await _genreService.GetGenreByIdAsync(id, cancellationToken);
        return Ok(genre);
    }

    [HttpPost("getAll")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<GenresResponseDto>> GetAllGenres(
        GetAllGenresRequestDto getAllGenresRequestDto,
        CancellationToken cancellationToken = default)
    {
        var genres = await _genreService.GetAllGenresAsync(getAllGenresRequestDto, cancellationToken);
        return Ok(genres);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateGenre(GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        await _genreService.CreateGenreAsync(genreRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateGenre(Guid id, GenreRequestDto genreRequestDto, CancellationToken cancellationToken)
    {
        await _genreService.UpdateGenreAsync(id, genreRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteGenre(Guid id, CancellationToken cancellationToken)
    {
        await _genreService.DeleteGenreAsync(id, cancellationToken);
        return NoContent();
    }
}