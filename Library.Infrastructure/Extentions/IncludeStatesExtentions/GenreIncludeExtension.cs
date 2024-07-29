using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Extentions.IncludeStatesExtentions;

public static class GenreIncludeExtension
{
    public static IQueryable<Genre> IncludeWithState(this IQueryable<Genre> query, GenreIncludeState includeState)
    {
        if (includeState.IncludeBooks)
        {
            query = query.Include(genre => genre.Books);
        }
        return query;
    }
}
