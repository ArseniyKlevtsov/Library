using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Extentions.IncludeStatesExtentions;

public static class RentedBookIncludeExtension
{
    public static IQueryable<RentedBook> IncludeWithState(this IQueryable<RentedBook> query, RentedBookIncludeState includeState)
    {
        if (includeState.IncludeBook)
        {
            query = query.Include(rentedBook => rentedBook.Book);
        }
        if (includeState.IncludeRentOrder)
        {
            query = query.Include(rentedBook => rentedBook.RentOrder);
        }
        return query;
    }
}
