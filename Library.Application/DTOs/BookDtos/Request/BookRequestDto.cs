namespace Library.Application.DTOs.BookDtos.Request;

public class BookRequestDto
{
    public string? Isbn { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }


    public Guid? AuthorId { get; set; }
    public ICollection<Guid>? GenreIds { get; set; }
}
