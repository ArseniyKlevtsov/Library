using Library.Domain.Interfaces;

namespace Library.Domain.Entities;

public class Book : IEntity
{
    public Guid Id {  get; set; }
    public string? Isbn { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public Guid InventoryId { get; set; }
    public LibraryInventory? Inventory { get; set; }

    public Guid AuthorId { get; set; }
    public Author? Author { get; set; }

    public ICollection<Genre>? Genres { get; set; }
    public ICollection<RentedBook>? RentedBooks { get; set; }

}
