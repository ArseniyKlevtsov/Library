using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations;

internal class RentOrderConfiguration : IEntityTypeConfiguration<RentOrder>
{
    public void Configure(EntityTypeBuilder<RentOrder> builder)
    {
        builder
            .HasKey(rentOrder => rentOrder.Id);

        builder
            .HasMany(rentOrer => rentOrer.RentedBooks)
            .WithOne(rentedBook => rentedBook.RentOrder)
            .HasForeignKey(rentedBook => rentedBook.RentOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
