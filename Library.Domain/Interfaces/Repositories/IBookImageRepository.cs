using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface IBookImageRepository : IRepository<BookImage>
{
    Task<IEnumerable<BookImage>> GetWithIncludeAsync(BookImageIncludeState includeState, CancellationToken cancellationToken);

    Task<IEnumerable<BookImage>> GetWithIncludeByPredicateAsync(Expression<Func<BookImage, bool>> predicate, BookImageIncludeState includeState, CancellationToken cancellationToken);

}
