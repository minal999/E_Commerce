using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [ScaffoldColumn(false)]
        public int User_Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string? F_Name { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string? L_Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Contact is required")]
        public string? Contact { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State  is required")]
        public string? State { get; set; }

        [Display(Name = "role")]
        public int Role_Id { get; set; }

        [ForeignKey("Role_Id")]
        public virtual Role? roles { get; set; }

    }
}
