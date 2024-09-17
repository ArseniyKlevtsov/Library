using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.SearchCriterias;
using Library.Infrastructure.Data;
using Library.Infrastructure.Extentions.IncludeStatesExtentions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    public BookRepository(LibraryDbContext context) : base(context) { }

    public async Task<IEnumerable<Book>> GetWithIncludeAsync(BookIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetWithIncludeByPredicateAsync(Expression<Func<Book, bool>> predicate, BookIncludeState includeState, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(predicate)
            .IncludeWithState(includeState)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetBooksWithCriterias(BookCriterieas bookCriterieas , CancellationToken cancellationToken)
    {
        IQueryable<Book> query = _dbSet;

        // Filter
        if (!string.IsNullOrEmpty(bookCriterieas.BookNameFilter))
        {
            query = query.Where(b => b.Name!.Contains(bookCriterieas.BookNameFilter));
        }
        if (!string.IsNullOrEmpty(bookCriterieas.IsbnFilter))
        {
            query = query.Where(b => b.Isbn!.Contains(bookCriterieas.IsbnFilter));
        }
        if (bookCriterieas.AuthorId.HasValue)
        {
            query = query.Where(b => b.AuthorId == bookCriterieas.AuthorId.Value);
        }
        if (bookCriterieas.GenreId.HasValue)
        {
            query = query.Where(b => b.Genres!.Any(g => g.Id == bookCriterieas.GenreId.Value));
        }

        if (bookCriterieas.RentOrderId.HasValue)
        {
            query = query.Where(b => b.RentedBooks!.Any(rb => rb.RentOrderId == bookCriterieas.RentOrderId.Value));
        }

        // Sort
        if (bookCriterieas.BookNameSortAsc.HasValue && bookCriterieas.BookNameSortAsc.Value)
        {
            query = query.OrderBy(b => b.Name);
        }
        else if (bookCriterieas.BookNameSortAsc.HasValue && !bookCriterieas.BookNameSortAsc.Value)
        {
            query = query.OrderByDescending(b => b.Name);
        }

        if (bookCriterieas.IsbnSortAsc.HasValue && bookCriterieas.IsbnSortAsc.Value)
        {
            query = query.OrderBy(b => b.Isbn);
        }
        else if (bookCriterieas.IsbnSortAsc.HasValue && !bookCriterieas.IsbnSortAsc.Value)
        {
            query = query.OrderByDescending(b => b.Isbn);
        }

        // Pagination
        if (bookCriterieas.Page.HasValue && bookCriterieas.PageSize.HasValue)
        {
            var skip = (bookCriterieas.Page.Value - 1) * bookCriterieas.PageSize.Value;
            return await query
                .AsNoTracking()
                .Skip(skip)
                .Take(bookCriterieas.PageSize.Value)
                .ToListAsync(cancellationToken);
        }

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

}
