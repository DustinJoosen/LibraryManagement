using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Helpers;

namespace LibraryManagement.Models
{
    public class Book : IImageContainable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        [StringLength(int.MaxValue)]
        public string? Description { get; set; }

        [Display(Name = "Cover Image")]
        public string? CoverImage { get; set; }

        [NotMapped]
        public string ImagePath
        {
            get => CoverImage?.ToString();
            set => CoverImage = value;
        }

        [StringLength(30)]
        public string? Language { get; set; }

        [Required]
        public int Pages { get; set; }

        [Required]
        [Display(Name = "Author")]
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        [NotMapped]
        [Display(Name = "Cover Image")]
        public IFormFile FormFile { get; set; }

        [NotMapped]
        public virtual ICollection<BookHistory>? BookHistories { get; set; }
    }
}