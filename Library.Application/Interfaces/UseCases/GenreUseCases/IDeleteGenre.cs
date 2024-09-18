namespace Library.Application.Interfaces.UseCases.GenreUseCases;

public interface IDeleteGenre
{
    Task ExecuteAsync(Guid id, CancellationToken cancellationToken);
}
