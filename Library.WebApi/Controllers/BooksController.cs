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
    private readonly ICreateBook _createBook;
    private readonly IDeleteBook _deleteBook;
    private readonly IUpdateBook _updateBook;
    private readonly IGetBookById _getBookById;
    private readonly IGetBooksPage _getBooksPage;

    public BooksController(ICreateBook createBook, IDeleteBook deleteBook, IUpdateBook updateBook, IGetBookById getBookById, IGetBooksPage getBooksPage)
    {
        _createBook = createBook;
        _deleteBook = deleteBook;
        _updateBook = updateBook;
        _getBookById = getBookById;
        _getBooksPage = getBooksPage;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<BookResponseDto>> GetBookById(Guid id, CancellationToken cancellationToken)
    {
        var book = await _getBookById.ExecuteAsync(id, cancellationToken);
        return Ok(book);
    }

    [HttpPost("getAll")]
    //[Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<BooksResponseDto>> GetAllBooks(
        GetAllBooksRequestDto getAllBooksRequestDto,
        CancellationToken cancellationToken = default)
    {
        var books = await _getBooksPage.ExecuteAsync(getAllBooksRequestDto, cancellationToken);
        return Ok(books);
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult<BookResponseDto>> CreateBook(BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var createdBook = await _createBook.ExecuteAsync(bookRequestDto, cancellationToken);
        return Ok(createdBook);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateBook(Guid id, BookRequestDto bookRequestDto, CancellationToken cancellationToken)
    {
        var updatedBook = await _updateBook.ExecuteAsync(id, bookRequestDto, cancellationToken);
        return Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
    {
        await _deleteBook.ExecuteAsync(id, cancellationToken);
        return NoContent();
    }
}