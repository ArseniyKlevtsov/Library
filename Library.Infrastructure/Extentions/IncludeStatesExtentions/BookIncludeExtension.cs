using Library.Domain.Entities;
using Library.Domain.IncludeStates;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Extentions.IncludeStatesExtentions;

public static class BookIncludeExtension
{
    public static IQueryable<Book> IncludeWithState(this IQueryable<Book> query, BookIncludeState includeState)
    {
        if (includeState.IncludeGenres)
        {
            query = query.Include(book => book.Genres);
        }
        if (includeState.IncludeInventory)
        {
            query = query.Include(book => book.Inventory);
        }
        if (includeState.IncludeRentedBooks)
        {
            query = query.Include(book => book.RentedBooks);
        }
        if (includeState.IncludeBookImage)
        {
            query = query.Include(book => book.BookImage);
        }
        if (includeState.IncludeAuthors)
        {
            query = query.Include(book => book.Author);
        }
        return query;
    }
}
