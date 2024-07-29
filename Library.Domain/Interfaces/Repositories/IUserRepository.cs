using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetWithIncludeAsync(UserIncludeState includeState, CancellationToken cancellationToken);

    Task<IEnumerable<User>> GetWithIncludeByPredicateAsync(Expression<Func<User, bool>> predicate, UserIncludeState includeState, CancellationToken cancellationToken);

}
