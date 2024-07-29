using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Extentions.IncludeStatesExtentions;

public static class RentOrderIncludeExtension
{
    public static IQueryable<RentOrder> IncludeWithState(this IQueryable<RentOrder> query, RentOrderIncludeState includeState)
    {
        if (includeState.IncludeUser)
        {
            query = query.Include(rentOrder => rentOrder.User);
        }
        if (includeState.IncludeRentedBooks)
        {
            query = query.Include(rentOrder => rentOrder.RentedBooks);
        }
        return query;
    }
}
