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

            // Generate the guids in advance, so that they can be refered by eachother.
            var authorGuids = Enumerable.Range(0, 4).Select(_ => Guid.NewGuid()).ToList();
            var genreGuids = Enumerable.Range(0, 3).Select(_ => Guid.NewGuid()).ToList();
            var userGuids = Enumerable.Range(0, 2).Select(_ => Guid.NewGuid()).ToList();
            var bookGuids = Enumerable.Range(0, 7).Select(_ => Guid.NewGuid()).ToList();

            modelBuilder.Entity<Author>()
                .HasData(new List<Author>
                {
                    new Author()
                    {
                        Id = authorGuids[0],
                        FirstName = "Andy",
                        LastName = "Weir"
                    },
                    new Author()
                    {
                        Id = authorGuids[1],
                        FirstName = "Suzanne",
                        LastName = "Collins"
                    },
                    new Author()
                    {
                        Id = authorGuids[2],
                        FirstName = "Joanne",
                        LastName = "Rowling"
                    },
                    new Author()
                    {
                        Id = authorGuids[3],
                        FirstName = "Cassandra",
                        LastName = "Clare"
                    }
                });

            modelBuilder.Entity<Genre>()
                .HasData(new List<Genre>
                {
                    new Genre()
                    {
                        Id = genreGuids[0],
                        Name = "SCI-FI",
                        Description = "Sience fiction"
                    },
                    new Genre()
                    {
                        Id = genreGuids[1],
                        Name = "Fantasy",
                        Description = "Dragons, mythical quests."
                    },
                    new Genre()
                    {
                        Id = genreGuids[2],
                        Name = "Dystopian"
                    },
                });

            modelBuilder.Entity<User>()
                .HasData(new List<User>
                {
                    new User()
                    {
                        Id = userGuids[0],
                        Email = "dustinjoosen2003@gmail.com",
                        Password = "Password123",
                        FirstName = "Dustin",
                        LastName = "Joosen",
                        Role = UserRole.Admin
                    },
                    new User()
                    {
                        Id = userGuids[1],
                        Email = "kycoven@gmail.com",
                        Password = "Password123",
                        FirstName = "Ky",
                        LastName = "Coven",
                        Role = UserRole.Customer
                    },
                });

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookHistory> BookHistories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
