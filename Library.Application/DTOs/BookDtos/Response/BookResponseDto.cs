using Library.Domain.Entities;

namespace Library.Application.DTOs.BookDtos.Response;

public class BookResponseDto
{
    public string? Isbn { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public Guid BookImageId { get; set; }
    public BookImage? BookImage { get; set; }

    public Guid InventoryId { get; set; }
    public LibraryInventory? Inventory { get; set; }

    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }

    public ICollection<Genre>? Genres { get; set; }
    public ICollection<RentedBook>? RentedBooks { get; set; }
}
