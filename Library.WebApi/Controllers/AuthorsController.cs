using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<AuthorResponseDto>> GetAuthorById(Guid id, CancellationToken cancellationToken)
    {
        var author = await _authorService.GetAuthorByIdAsync(id, cancellationToken);
        return Ok(author);
    }

    [HttpPost("getAll")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<AuthorResponseDto>> GetAllAuthors(GetAllAuthorsRequestDto getAllAuthorsRequestDto, CancellationToken cancellationToken = default)
    {
        var authors = await _authorService.GetAllAuthorsAsync(getAllAuthorsRequestDto, cancellationToken);
        return Ok(authors);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateAuthor(AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        await _authorService.CreateAuthorAsync(authorRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateAuthor(Guid id, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        await _authorService.UpdateAuthorAsync(id, authorRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteAuthor(Guid id, CancellationToken cancellationToken)
    {
        await _authorService.DeleteAuthorAsync(id, cancellationToken);
        return NoContent();
    }
}