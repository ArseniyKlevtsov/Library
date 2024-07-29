using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data;
using Library.Infrastructure.Extentions.IncludeStatesExtentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<User>> GetWithIncludeAsync(UserIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetWithIncludeByPredicateAsync(Expression<Func<User, bool>> predicate, UserIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }
}
