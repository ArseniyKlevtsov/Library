namespace Library.Application.DTOs.BookDtos.Response;

public class BookPreviewResponseDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int? AvailableCount { get; set; }
    public int? TotalCount { get; set; }
}
