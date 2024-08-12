namespace Library.Application.DTOs.AuthorDtos.Request;

public class AuthorRequestDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Country { get; set; }
}
