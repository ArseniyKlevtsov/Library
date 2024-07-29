using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data;
using Library.Infrastructure.Extentions.IncludeStatesExtentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class BookImageRepository : BaseRepository<BookImage>, IBookImageRepository
{
    public BookImageRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<BookImage>> GetWithIncludeAsync(BookImageIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<BookImage>> GetWithIncludeByPredicateAsync(Expression<Func<BookImage, bool>> predicate, BookImageIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }
}
