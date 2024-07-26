using Library.Domain.Interfaces;

namespace Library.Domain.Entities;

public class LibraryInventory: IEntity
{
    public Guid Id { get; set; }
    public int AvailableCount { get; set; }
    public int TotalCount { get; set; }

    public Guid BookId { get; set; }
    public Book? Book { get; set; }

}
