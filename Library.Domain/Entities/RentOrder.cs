using Library.Domain.Interfaces;

namespace Library.Domain.Entities;

public class RentOrder : IEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public ICollection<RentedBook>? RentedBooks { get; set; }
}
