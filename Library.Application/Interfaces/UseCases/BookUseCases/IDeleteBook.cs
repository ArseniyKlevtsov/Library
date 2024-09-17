namespace Library.Application.Interfaces.UseCases.BookUseCases;

public interface IDeleteBook
{
    Task ExecuteAsync(Guid id, CancellationToken cancellationToken);
}
