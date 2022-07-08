using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? CoverImage { get; set; }

        [StringLength(30)]
        public string? Language { get; set; }

        [Required]
        public int Pages { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        [Required]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        [NotMapped]
        public virtual ICollection<BookHistory>? BookHistories { get; set; }
    }
}