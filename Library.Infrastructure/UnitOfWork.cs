using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Library.Infrastructure;

public class UnitOfWork : IDisposable
{
    private bool disposed = false;

    private readonly LibraryDbContext _libraryDbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;


    private IAccountManager? accountManager;
    private IAuthorRepository? authorRepository;
    private IBookRepository? bookRepository;
    private IBookImageRepository? bookImageRepository;
    private IGenreRepository? genreRepository;
    private ILibraryInventoryRepository? libraryInventoryRepository;
    private IRentOrderRepository? rentOrderRepository;
    private IRentedBookRepository? rentedBookRepository;
    private IUserRepository? userRepository;

    public UnitOfWork(LibraryDbContext libraryDbContext, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _libraryDbContext = libraryDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IAccountManager AccountManager
    {
        get
        {
            if (accountManager == null)
                accountManager = new AccountManager(_userManager, _roleManager);
            return accountManager;
        }
    }

    public IAuthorRepository Authors
    {
        get
        {
            if (authorRepository == null)
                authorRepository = new AuthorRepository(_libraryDbContext);
            return authorRepository;
        }
    }

    public IBookRepository Books
    {
        get
        {
            if (bookRepository == null)
                bookRepository = new BookRepository(_libraryDbContext);
            return bookRepository;
        }
    }

    public IBookImageRepository BookImages
    {
        get
        {
            if (bookImageRepository == null)
                bookImageRepository = new BookImageRepository(_libraryDbContext);
            return bookImageRepository;
        }
    }

    public IGenreRepository Genres
    {
        get
        {
            if (genreRepository == null)
                genreRepository = new GenreRepository(_libraryDbContext);
            return genreRepository;
        }
    }

    public ILibraryInventoryRepository LibraryInventorys
    {
        get
        {
            if (libraryInventoryRepository == null)
                libraryInventoryRepository = new LibraryInventoryRepository(_libraryDbContext);
            return libraryInventoryRepository;
        }
    }

    public IRentOrderRepository RentOrders
    {
        get
        {
            if (rentOrderRepository == null)
                rentOrderRepository = new RentOrderRepository(_libraryDbContext);
            return rentOrderRepository;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (userRepository == null)
                userRepository = new UserRepository(_libraryDbContext);
            return userRepository;
        }
    }

    public void Save()
    {
        _libraryDbContext.SaveChanges();
    }

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _libraryDbContext.Dispose();
            }
            this.disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
