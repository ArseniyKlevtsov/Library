using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations;

internal class BookImageConfiguration : IEntityTypeConfiguration<BookImage>
{
    public void Configure(EntityTypeBuilder<BookImage> builder)
    {
        builder
            .HasKey(bookImage => bookImage.Id);

        builder
            .Property(bookImage => bookImage.Image)
            .HasColumnType("image");

        builder
            .HasOne(bookImage => bookImage.Book)
            .WithOne(book => book.BookImage)
            .HasForeignKey<Book>(book => book.BookImageId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
