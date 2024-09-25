namespace Library.Application.Exceptions;

public class NoAvailableQuantityException: Exception
{
    public NoAvailableQuantityException(string message) : base(message) { }
}
