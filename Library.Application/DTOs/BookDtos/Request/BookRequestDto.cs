namespace Library.Application.DTOs.BookDtos.Request;

public class BookRequestDto
{
    public string? Isbn { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public Guid? AuthorId { get; set; }

    public string? Image { get; set; }

    public int? AvailableCount { get; set; }
    public int? TotalCount { get; set; }

    public ICollection<Guid>? GenreIds { get; set; }
}
