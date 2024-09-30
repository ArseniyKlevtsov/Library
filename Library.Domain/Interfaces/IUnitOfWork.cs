using Library.Domain.Interfaces.Repositories;

namespace Library.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAccountManager AccountManager { get; }
    IAuthorRepository Authors { get; }
    IBookRepository Books { get; }
    IBookImageRepository BookImages { get; }
    IGenreRepository Genres { get; }
    ILibraryInventoryRepository LibraryInventorys { get; }
    IRentOrderRepository RentOrders { get; }
    IRentedBookRepository RentedBooks { get; }
    IUserRepository Users { get; }

    Task SaveAsync();
}