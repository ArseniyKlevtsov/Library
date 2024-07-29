using Library.Domain.Interfaces;

namespace Library.Domain.IncludeStates;

public class BookIncludeState : IIncludeState
{
    public bool IncludeBookImage { get; set; }
    public bool IncludeInventory { get; set; }
    public bool IncludeGenres { get; set; }
    public bool IncludeRentedBooks { get; set; }
}
