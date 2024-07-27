using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(user => user.Id);

        builder
            .Property(user => user.UserName)
            .IsRequired();

        builder
            .Property(user => user.Email)
            .IsRequired();

        builder
            .HasMany(user => user.RentOrders)
            .WithOne(rentOrder => rentOrder.User)
            .HasForeignKey( rentOrder => rentOrder.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
