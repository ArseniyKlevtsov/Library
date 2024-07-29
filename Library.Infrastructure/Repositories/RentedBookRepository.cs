using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data;
using Library.Infrastructure.Extentions.IncludeStatesExtentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class RentedBookRepository : BaseRepository<RentedBook>, IRentedBookRepository
{
    public RentedBookRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<RentedBook>> GetWithIncludeAsync(RentedBookIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RentedBook>> GetWithIncludeByPredicateAsync(Expression<Func<RentedBook, bool>> predicate, RentedBookIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }
}
