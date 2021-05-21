using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasOne(s => s.Author)
                .WithMany(u => u.Books)
                .HasForeignKey(s => s.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}