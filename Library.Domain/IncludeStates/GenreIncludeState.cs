using Library.Domain.Interfaces;

namespace Library.Domain.IncludeStates;

public class GenreIncludeState : IIncludeState
{
    public bool IncludeBooks { get; set; }
}
