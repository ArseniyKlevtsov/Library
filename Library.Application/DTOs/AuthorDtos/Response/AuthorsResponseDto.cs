namespace Library.Application.DTOs.AuthorDtos.Response;

public class AuthorsResponseDto
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<AuthorResponseDto>? Authors { get; set; }
}
