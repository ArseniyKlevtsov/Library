using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<BookResponseDto>> GetBookById(Guid id, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetBookByIdAsync(id, cancellationToken);
        return Ok(book);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetAllBooks(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var books = await _bookService.GetAllBooksAsync(pageNumber, pageSize, cancellationToken);
        return Ok(books);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateBook(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        await _bookService.CreateBookAsync(bookRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateBook(Guid id, BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        await _bookService.UpdateBookAsync(id, bookRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
    {
        await _bookService.DeleteBookAsync(id, cancellationToken);
        return NoContent();
    }
}