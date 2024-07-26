using Library.Domain.Interfaces;

namespace Library.Domain.Entities;

public class BookImage : IEntity
{
    public Guid Id { get; set; }
    public byte[]? Image { get; set; }

    public Guid BookId { get; set; }
    public Book? Book { get; set; }
}
