using Library.Application.DTOs.AuthorDtos.Request;
using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.Interfaces.UseCases.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly ICreateAuthor _createAuthor;
    private readonly IUpdateAuthor _updateAuthor;
    private readonly IDeleteAuthor _deleteAuthor;
    private readonly IGetAuthorById _getAuthorById;
    private readonly IGetAuthorsPage _getAuthorsPage;

    public AuthorsController(ICreateAuthor createAuthor, IDeleteAuthor deleteAuthor, IGetAuthorById getAuthorById, IGetAuthorsPage getAuthorsPage, IUpdateAuthor updateAuthor)
    {
        _createAuthor = createAuthor;
        _updateAuthor = updateAuthor;
        _deleteAuthor = deleteAuthor;
        _getAuthorById = getAuthorById;
        _getAuthorsPage = getAuthorsPage;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<AuthorResponseDto>> GetAuthorById(Guid id, CancellationToken cancellationToken)
    {
        var author = await _getAuthorById.ExecuteAsync(id, cancellationToken);
        return Ok(author);
    }

    [HttpPost("getAll")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<AuthorResponseDto>> GetAllAuthors(GetAllAuthorsRequestDto getAllAuthorsRequestDto, CancellationToken cancellationToken = default)
    {
        var authors = await _getAuthorsPage.ExecuteAsync(getAllAuthorsRequestDto, cancellationToken);
        return Ok(authors);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AuthorResponseDto>> CreateAuthor(AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        var createdAuthor = await _createAuthor.ExecuteAsync(authorRequestDto, cancellationToken);
        return Ok(createdAuthor);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AuthorResponseDto>> UpdateAuthor(Guid id, AuthorRequestDto authorRequestDto, CancellationToken cancellationToken)
    {
        var updatedAuthor = await _updateAuthor.ExecuteAsync(id, authorRequestDto, cancellationToken);
        return Ok(updatedAuthor);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteAuthor(Guid id, CancellationToken cancellationToken)
    {
        await _deleteAuthor.ExecuteAsync(id, cancellationToken);
        return Ok();
    }
}