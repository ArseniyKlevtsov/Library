using Library.Domain.Interfaces;

namespace Library.Domain.IncludeStates;

public class BookImageIncludeState : IIncludeState
{
    public bool IncludeImage { get; set; }
    public bool IncludeBook { get; set; }
}
