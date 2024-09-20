using Library.Application.DTOs.AuthorDtos.Response;
using Library.Application.DTOs.GenreDtos.Response;

namespace Library.Application.DTOs.BookDtos.Response;

public class BookInfoResponseDto
{
    public Guid Id { get; set; }
    public string? Isbn { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public AuthorResponseDto? Author { get; set; }

    public string? Image { get; set; }

    public int? AvailableCount { get; set; }
    public int? TotalCount { get; set; }

    public IEnumerable<GenreResponseDto>? Genres { get; set; }
}
