namespace Library.Application.Exceptions;

public class ReadTokenException : Exception
{
    public ReadTokenException (string message) : base (message) { }
}
