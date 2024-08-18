namespace Library.Application.DTOs.GenreDtos.Response;

public class GenresResponseDto
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<GenreResponseDto>? Genres { get; set; }
}
