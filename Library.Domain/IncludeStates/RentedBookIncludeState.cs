using Library.Domain.Interfaces;

namespace Library.Domain.IncludeStates;

public class RentedBookIncludeState : IIncludeState
{
    public bool IncludeBook { get; set; }
    public bool IncludeRentOrder { get; set; }
}
