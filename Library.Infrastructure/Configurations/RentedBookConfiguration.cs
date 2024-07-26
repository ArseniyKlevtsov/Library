using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations;

internal class RentedBookConfiguration : IEntityTypeConfiguration<RentedBook>
{
    public void Configure(EntityTypeBuilder<RentedBook> builder)
    {
        builder
            .HasKey(rentedBook => rentedBook.Id);

        builder
            .Property(rentedBook => rentedBook.BooksCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder
            .HasOne(rentedBook => rentedBook.Book)
            .WithMany(book => book.RentedBooks)
            .HasForeignKey(book => book.BookId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(rentedBook => rentedBook.RentOrder)
            .WithMany(rentOrder => rentOrder.RentedBooks)
            .HasForeignKey(book => book.RentOrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
