using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Extentions.IncludeStatesExtentions;

public static class BookImageIncludeExtension
{
    public static IQueryable<BookImage> IncludeWithState(this IQueryable<BookImage> query, BookImageIncludeState includeState)
    {
        if (includeState.IncludeBook)
        {
            query = query.Include(bookImage => bookImage.Book);
        }
        if (includeState.IncludeImage)
        {
            query = query.Include(bookImage => bookImage.Image);
        }
        return query;
    }
}
