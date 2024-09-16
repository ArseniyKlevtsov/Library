namespace Library.Application.Interfaces.UseCases.Authors;

public interface IDeleteAuthor
{
    Task ExecuteAsync(Guid id, CancellationToken cancellationToken);
}
