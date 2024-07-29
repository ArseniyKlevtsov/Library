using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface IRentedBookRepository : IRepository<RentedBook>
{
    Task<IEnumerable<RentedBook>> GetWithIncludeAsync(RentedBookIncludeState includeState, CancellationToken cancellationToken);

    Task<IEnumerable<RentedBook>> GetWithIncludeByPredicateAsync(Expression<Func<RentedBook, bool>> predicate, RentedBookIncludeState includeState, CancellationToken cancellationToken);

}
