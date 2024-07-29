using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Extentions.IncludeStatesExtentions;

public static class UserIncludeExtension
{
    public static IQueryable<User> IncludeWithState(this IQueryable<User> query, UserIncludeState includeState)
    {
        if (includeState.IncludeRentOrders)
        {
            query = query.Include(user => user.RentOrders);
        }
        return query;
    }
}
