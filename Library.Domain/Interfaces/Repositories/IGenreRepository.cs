using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task<IEnumerable<Genre>> GetWithIncludeAsync(GenreIncludeState includeState, CancellationToken cancellationToken);

    Task<IEnumerable<Genre>> GetWithIncludeByPredicateAsync(Expression<Func<Genre, bool>> predicate, GenreIncludeState includeState, CancellationToken cancellationToken);

}
