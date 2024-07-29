using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data;
using Library.Infrastructure.Extentions.IncludeStatesExtentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryDbContext context) : base(context) { }

    public async  Task<IEnumerable<Author>> GetWithIncludeAsync(AuthorIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Author>> GetWithIncludeByPredicateAsync(Expression<Func<Author, bool>> predicate, AuthorIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }
}
