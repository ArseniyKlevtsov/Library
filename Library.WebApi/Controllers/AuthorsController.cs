using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly ICreateAuthor _createAuthor;
    private readonly IDeleteAuthor _deleteAuthor;
    private readonly IGetAuthorById _getAuthorById;

    public AuthorsController(IAuthorService authorService, ICreateAuthor createAuthor, IDeleteAuthor deleteAuthor, IGetAuthorById getAuthorById)
    {
        _authorService = authorService;
        _createAuthor = createAuthor;
        _deleteAuthor = deleteAuthor;
        _getAuthorById = getAuthorById;
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<AuthorResponseDto>> GetAuthorById(Guid id, CancellationToken cancellationToken)
    {
        var author = await _getAuthorById.ExecuteAsync(id, cancellationToken);
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
    public async Task<ActionResult<AuthorResponseDto>> CreateAuthor(AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        await _authorService.CreateAuthorAsync(authorRequestDto, cancellationToken);
        var createdAuthor = await _createAuthor.ExecuteAsync(authorRequestDto, cancellationToken);
        return Ok(createdAuthor);
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
        await _deleteAuthor.ExecuteAsync(id, cancellationToken);
        return Ok();
    }
}