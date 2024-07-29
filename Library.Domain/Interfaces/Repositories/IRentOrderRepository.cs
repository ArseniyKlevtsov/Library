using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface IRentOrderRepository : IRepository<RentOrder>
{
    Task<IEnumerable<RentOrder>> GetWithIncludeAsync(RentOrderIncludeState includeState, CancellationToken cancellationToken);

    Task<IEnumerable<RentOrder>> GetWithIncludeByPredicateAsync(Expression<Func<RentOrder, bool>> predicate, RentOrderIncludeState includeState, CancellationToken cancellationToken);

}
