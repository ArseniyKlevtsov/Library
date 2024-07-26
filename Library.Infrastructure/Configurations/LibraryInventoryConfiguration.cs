using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations;

internal class LibraryInventoryConfiguration : IEntityTypeConfiguration<LibraryInventory>
{
    public void Configure(EntityTypeBuilder<LibraryInventory> builder)
    {
        builder
            .HasKey(libraryInventory => libraryInventory.Id);

        builder
            .Property(libraryInventory => libraryInventory.TotalCount)
            .HasDefaultValue(0);

        builder
            .Property(libraryInventory => libraryInventory.AvailableCount)
            .HasDefaultValue(0);

        builder
            .HasOne(libraryInventory => libraryInventory.Book)
            .WithOne(book => book.Inventory)
            .HasForeignKey<Book>(book => book.InventoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
