using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Configurations
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(s => s.Employee)
                .WithMany(u => u.Orders)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(s => s.Customer)
                .WithMany(u => u.Orders)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(s => s.Book)
                .WithMany(u => u.Orders)
                .HasForeignKey(s => s.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}