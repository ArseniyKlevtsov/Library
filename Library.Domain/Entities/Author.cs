using Library.Domain.Interfaces;

namespace Library.Domain.Entities;

public class Author : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? Country { get; set; }

    public ICollection<Book>? Boocks { get; set; }

}
