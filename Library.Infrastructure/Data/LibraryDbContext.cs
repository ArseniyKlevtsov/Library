using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data;

public class LibraryDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Author>? Authors { get; set; }
    public DbSet<Book>? Books { get; set; }
    public DbSet<BookImage>? BookImages { get; set; }
    public DbSet<Genre>? Genres { get; set; }
    public DbSet<LibraryInventory>? LibraryInventories { get; set; }
    public DbSet<RentedBook>? RentedBooks { get; set; }
    public DbSet<RentOrder>? RentOrders { get; set; }
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
    }

}
