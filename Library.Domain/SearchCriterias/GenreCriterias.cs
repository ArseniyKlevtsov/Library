namespace Library.Domain.SearchCriterias;

public class GenreCriterias
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public bool? NameSortAsc { get; set; }

    public string? NameFilter { get; set; }
    public Guid? BookId { get; set; }
}
