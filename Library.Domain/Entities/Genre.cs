using Library.Domain.Interfaces;

namespace Library.Domain.Entities;

public class Genre: IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Book>? Books { get; set; }
}
