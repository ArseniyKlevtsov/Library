namespace Library.Domain.SearchCriterias;

public class BookCriterieas
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public bool? BookNameSortAsc { get; set; }
    public bool? IsbnSortAsc { get; set; }

    public string? BookNameFilter { get; set; }
    public bool? IsbnSortFilter { get; set; }

    public Guid? AuthorId { get; set; }
    public Guid? GenreId { get; set; }
    public Guid? RentOrderId { get; set; }
}
