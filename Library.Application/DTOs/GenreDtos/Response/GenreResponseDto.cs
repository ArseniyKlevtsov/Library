using Library.Domain.Entities;

namespace Library.Application.DTOs.GenreDtos.Response;

public class GenreResponseDto
{
    public string? Name { get; set; }

    public ICollection<Book>? Books { get; set; }
}
