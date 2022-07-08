using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

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
