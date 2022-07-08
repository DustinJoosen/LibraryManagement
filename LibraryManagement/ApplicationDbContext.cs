using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.Entity<BookHistory>()
                .HasOne<User>(bookhistory => bookhistory.User)
                .WithMany(user => user.BookHistories)
                .HasForeignKey(bookhistory => bookhistory.UserId);

            modelBuilder.Entity<BookHistory>()
                .HasOne<Book>(bookhistory => bookhistory.Book)
                .WithMany(book => book.BookHistories)
                .HasForeignKey(bookhistory => bookhistory.BookId);
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookHistory> BookHistories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
