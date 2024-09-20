using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.SearchCriterias;
using Library.Infrastructure.Data;
using Library.Infrastructure.Extentions.IncludeStatesExtentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class GenreRepository : BaseRepository<Genre>, IGenreRepository
{
    public GenreRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<Genre>> GetWithIncludeAsync(GenreIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Genre>> GetWithIncludeByPredicateAsync(
        Expression<Func<Genre, bool>> predicate,
        GenreIncludeState includeState,
        CancellationToken cancellationToken,
        bool allowTracking = false)
    {
        var query = _dbSet.AsQueryable();

        if (allowTracking == false)
        {
            query = query.AsNoTracking();
        }

        return await query
            .Where(predicate)
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Genre>> GetGenresWithCriterias(GenreCriterias genreCriterias, CancellationToken cancellationToken)
    {
        IQueryable<Genre> query = _dbSet;

        // filter
        if (!string.IsNullOrEmpty(genreCriterias.NameFilter))
        {
            query = query.Where(g => g.Name!.Contains(genreCriterias.NameFilter));
        }
        if (genreCriterias.BookId.HasValue)
        {
            query = query.Where(g => g.Books!.Any(b => b.Id == genreCriterias.BookId.Value));
        }

        // sort
        if (genreCriterias.NameSortAsc.HasValue)
        {
            query = genreCriterias.NameSortAsc.Value ?
                query.OrderBy(g => g.Name) :
                query.OrderByDescending(g => g.Name);
        }

        // pagination
        if (genreCriterias.Page.HasValue && genreCriterias.PageSize.HasValue)
        {
            var skip = (genreCriterias.Page.Value - 1) * genreCriterias.PageSize.Value;
            return await query
                .AsNoTracking()
                .Skip(skip)
                .Take(genreCriterias.PageSize.Value)
                .ToListAsync(cancellationToken);
        }

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
