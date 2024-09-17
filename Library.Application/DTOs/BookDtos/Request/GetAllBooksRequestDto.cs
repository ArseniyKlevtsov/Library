namespace Library.Application.DTOs.BookDtos.Request;

public class GetAllBooksRequestDto
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public bool? BookNameSortAsc { get; set; }
    public bool? IsbnSortAsc { get; set; }

    public string? BookNameFilter { get; set; }
    public string? IsbnFilter { get; set; }

    public Guid? AuthorId { get; set; }
    public Guid? GenreId { get; set; }
    public Guid? RentOrderId { get; set; }
}
