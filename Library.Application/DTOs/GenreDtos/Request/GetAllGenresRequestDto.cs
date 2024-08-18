namespace Library.Application.DTOs.GenreDtos.Request;

public class GetAllGenresRequestDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public bool? NameSortAsc { get; set; }

    public string? NameFilter { get; set; }
    public Guid? BookId { get; set; }
}
