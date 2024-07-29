using Library.Domain.Interfaces;

namespace Library.Domain.IncludeStates;

public class UserIncludeState : IIncludeState
{
    public bool IncludeRentOrders { get; set; }
}
