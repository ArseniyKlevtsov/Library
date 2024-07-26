using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations;

internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
            .HasKey(genre => genre.Id);

        builder
            .Property(genre => genre.Name)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .HasMany(genre => genre.Books)
            .WithMany(book => book.Genres);
    }
}
