using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Extentions.IncludeStatesExtentions;

public static class LibraryInventoryIncludeExtension
{
    public static IQueryable<LibraryInventory> IncludeWithState(this IQueryable<LibraryInventory> query, LibraryInventoryIncludeState includeState)
    {
        if (includeState.IncludeBook)
        {
            query = query.Include(lbraryInventory => lbraryInventory.Book);
        }
        return query;
    }
}
