namespace Library.Application.DTOs.BookDtos.Response;

public class BooksResponseDto
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<BookResponseDto>? Books { get; set; }
}
