using Library.Domain.Interfaces;

namespace Library.Domain.IncludeStates;

public class AuthorIncludeState: IIncludeState
{
    public bool IncludeBooks { get; set; }
}
