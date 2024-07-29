using Library.Domain.Interfaces;

namespace Library.Domain.IncludeStates;

public class LibraryInventoryIncludeState : IIncludeState
{
    public bool IncludeBook { get; set; }
}
