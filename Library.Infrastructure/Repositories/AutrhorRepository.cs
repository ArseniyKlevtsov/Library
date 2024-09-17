using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.SearchCriteries;
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

    public async Task<IEnumerable<Author>> GetAuthorsWithCriterias ( AuthorCriterias authorCriterias, CancellationToken cancellationToken)
    {
        IQueryable<Author> query = _dbSet;

        // filter
        if (!string.IsNullOrEmpty(authorCriterias.NameFilter))
        {
            query = query.Where(a => a.Name!.Contains(authorCriterias.NameFilter));
        }
        if (!string.IsNullOrEmpty(authorCriterias.SurnameFilter))
        {
            query = query.Where(a => a.Surname!.Contains(authorCriterias.SurnameFilter));
        }
        if (authorCriterias.BirthDateFilter.HasValue)
        {
            query = query.Where(a => a.BirthDate == authorCriterias.BirthDateFilter.Value);
        }
        if (!string.IsNullOrEmpty(authorCriterias.CountryFilter))
        {
            query = query.Where(a => a.Country!.Contains(authorCriterias.CountryFilter));
        }
        if (authorCriterias.BookId.HasValue)
        {
            query = query.Where(a => a.Books!.Any(b => b.Id == authorCriterias.BookId.Value));
        }

        // sort
        if (authorCriterias.NameSortAsc.HasValue && authorCriterias.NameSortAsc.Value)
        {
            query = query.OrderBy(a => a.Name);
        }
        else if (authorCriterias.NameSortAsc.HasValue && !authorCriterias.NameSortAsc.Value)
        {
            query = query.OrderByDescending(a => a.Name);
        }

        if (authorCriterias.SurnameSortAsc.HasValue && authorCriterias.SurnameSortAsc.Value)
        {
            query = query.OrderBy(a => a.Surname);
        }
        else if (authorCriterias.SurnameSortAsc.HasValue && !authorCriterias.SurnameSortAsc.Value)
        {
            query = query.OrderByDescending(a => a.Surname);
        }

        if (authorCriterias.BirthDateSortAsc.HasValue && authorCriterias.BirthDateSortAsc.Value)
        {
            query = query.OrderBy(a => a.BirthDate);
        }
        else if (authorCriterias.BirthDateSortAsc.HasValue && !authorCriterias.BirthDateSortAsc.Value)
        {
            query = query.OrderByDescending(a => a.BirthDate);
        }

        if (authorCriterias.CountrySortAsc.HasValue && authorCriterias.CountrySortAsc.Value)
        {
            query = query.OrderBy(a => a.Country);
        }
        else if (authorCriterias.CountrySortAsc.HasValue && !authorCriterias.CountrySortAsc.Value)
        {
            query = query.OrderByDescending(a => a.Country);
        }

        // pagination
        if (authorCriterias.Page.HasValue && authorCriterias.PageSize.HasValue)
        {
            var skip = (authorCriterias.Page.Value - 1) * authorCriterias.PageSize.Value;
            return await query
                .AsNoTracking()
                .Skip(skip)
                .Take(authorCriterias.PageSize.Value)
                .ToListAsync(cancellationToken);
        }

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}

