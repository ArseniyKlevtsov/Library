using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Library.Domain.SearchCriterias;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Book>> GetWithIncludeAsync(BookIncludeState includeState, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> GetWithIncludeByPredicateAsync(Expression<Func<Book, bool>> predicate, BookIncludeState includeState, CancellationToken cancellationToken);
    Task<IEnumerable<Book>> GetBooksWithCriterias(BookCriterieas bookCriterieas, CancellationToken cancellationToken);
}
