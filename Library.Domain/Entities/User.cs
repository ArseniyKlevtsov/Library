using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Library.Domain.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    public ICollection<RentOrder>? RentOrders { get; set; }
}
