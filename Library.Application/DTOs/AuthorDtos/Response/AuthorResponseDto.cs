namespace Library.Application.DTOs.AuthorDtos.Response;

public class AuthorResponseDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Country { get; set; }

    public ICollection<Guid>? BookIds { get; set; }
}
