using Library.Application.DTOs.BookDtos.Request;
using Library.Application.DTOs.BookDtos.Response;
using Library.Application.Interfaces.Services;
using Library.Application.Interfaces.UseCases.BookUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ICreateBook _createBook;
    private readonly IDeleteBook _deleteBook;
    private readonly IUpdateBook _updateBook;

    public BooksController(IBookService bookService, ICreateBook createBook, IDeleteBook deleteBook, IUpdateBook update)
    {
        _bookService = bookService;
        _createBook = createBook;
        _deleteBook = deleteBook;
        _updateBook = update;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<BookResponseDto>> GetBookById(Guid id, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetBookByIdAsync(id, cancellationToken);
        return Ok(book);
    }

    [HttpPost("getAll")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<BooksResponseDto>> GetAllBooks(
        GetAllBooksRequestDto getAllBooksRequestDto,
        CancellationToken cancellationToken = default)
    {
        var books = await _bookService.GetAllBooksAsync(getAllBooksRequestDto, cancellationToken);
        return Ok(books);
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateBook(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        await _createBook.ExecuteAsync(bookRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateBook(Guid id, BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        await _updateBook.ExecuteAsync(id, bookRequestDto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
    {
        await _deleteBook.ExecuteAsync(id, cancellationToken);
        return NoContent();
    }
}