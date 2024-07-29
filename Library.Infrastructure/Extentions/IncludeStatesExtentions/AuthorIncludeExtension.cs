using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Extentions.IncludeStatesExtentions;

public static class AuthorIncludeExtension
{
    public static IQueryable<Author> IncludeWithState(this IQueryable<Author> query, AuthorIncludeState includeState)
    {
        if (includeState.IncludeBooks)
        {
            query = query.Include(author => author.Books);
        }
        return query;
    }
}
