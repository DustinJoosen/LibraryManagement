using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "First name")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full name")]
        public string FullName 
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required]
        public UserRole Role { get; set; } = UserRole.Customer;

        [NotMapped]
        public virtual ICollection<BookHistory>? BookHistories { get; set; } 
    }

    public enum UserRole
    {
        Customer = 0,
        Admin = 1
    }
}
