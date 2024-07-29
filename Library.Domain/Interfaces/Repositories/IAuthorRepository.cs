using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<IEnumerable<Author>> GetWithIncludeAsync(AuthorIncludeState includeState, CancellationToken cancellationToken);

    Task<IEnumerable<Author>> GetWithIncludeByPredicateAsync(Expression<Func<Author, bool>> predicate, AuthorIncludeState includeState, CancellationToken cancellationToken);
}
