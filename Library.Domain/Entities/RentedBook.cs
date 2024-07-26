using Library.Domain.Interfaces;

namespace Library.Domain.Entities;

public class RentedBook : IEntity
{
    public Guid Id { get; set; }
    public DateTime? TakeTime { get; set; }
    public DateTime? ReturnTime { get; set; }
    public int BooksCount { get; set; }

    public Guid BookId { get; set; }
    public Book? Book { get; set; }

    public Guid RentOrderId { get; set; }
    public RentOrder? RentOrder { get; set; }
}
