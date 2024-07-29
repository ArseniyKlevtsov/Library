using Library.Domain.Interfaces;

namespace Library.Domain.IncludeStates;

public class RentOrderIncludeState : IIncludeState
{
    public bool IncludeUser { get; set; }
    public bool IncludeRentedBooks { get; set; }
}
