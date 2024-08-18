namespace Library.Application.DTOs.AuthorDtos.Request;

public class GetAllAuthorsRequestDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public bool? NameSortAsc { get; set; }
    public bool? SurnameSortAsc { get; set; }
    public bool? BirthDateSortAsc { get; set; }
    public bool? CountrySortAsc { get; set; }

    public string? NameFilter { get; set; }
    public string? SurnameFilter { get; set; }
    public DateOnly? BirthDateFilter { get; set; }
    public string? CountryFilter { get; set; }
    public Guid? BookId { get; set; }
}
