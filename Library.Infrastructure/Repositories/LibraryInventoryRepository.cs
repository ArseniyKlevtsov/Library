using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data;
using Library.Infrastructure.Extentions.IncludeStatesExtentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class LibraryInventoryRepository : BaseRepository<LibraryInventory>, ILibraryInventoryRepository
{
    public LibraryInventoryRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<LibraryInventory>> GetWithIncludeAsync(LibraryInventoryIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<LibraryInventory>> GetWithIncludeByPredicateAsync(Expression<Func<LibraryInventory, bool>> predicate, LibraryInventoryIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }
}
