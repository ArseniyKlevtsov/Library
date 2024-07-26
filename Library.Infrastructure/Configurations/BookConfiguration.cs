using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasKey(book => book.Id);

        builder
            .Property(book => book.Isbn)
            .IsRequired();

        builder
            .Property(book => book.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(book => book.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .HasOne(book => book.BookImage)
            .WithOne(bookImage => bookImage.Book)
            .HasForeignKey<BookImage>(bookImage => bookImage.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(book => book.Inventory)
            .WithOne(inventory => inventory.Book)
            .HasForeignKey<LibraryInventory>(inventory => inventory.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(book => book.Author)
            .WithMany(author => author.Books)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(book => book.Genres)
            .WithMany(genre => genre.Books);

        builder
            .HasMany(book => book.RentedBooks)
            .WithOne(renredBook => renredBook.Book)
            .HasForeignKey(renredBook => renredBook.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
