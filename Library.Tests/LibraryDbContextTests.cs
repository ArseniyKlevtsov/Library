using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Tests;

public class LibraryDbContextTests : LibraryDbContext
{
    public LibraryDbContextTests() : base(new DbContextOptionsBuilder<LibraryDbContext>()
        .UseInMemoryDatabase(databaseName: "LibraryDb")
        .Options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
    }
}
