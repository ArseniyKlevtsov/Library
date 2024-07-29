using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface ILibraryInventoryRepository : IRepository<LibraryInventory>
{
    Task<IEnumerable<LibraryInventory>> GetWithIncludeAsync(LibraryInventoryIncludeState includeState, CancellationToken cancellationToken);

    Task<IEnumerable<LibraryInventory>> GetWithIncludeByPredicateAsync(Expression<Func<LibraryInventory, bool>> predicate, LibraryInventoryIncludeState includeState, CancellationToken cancellationToken);

}
