using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [Table("role")]
    public class Role:Attribute
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Role_Id { get; set; }

        [Required(ErrorMessage ="Name is reauired")]
        public string? Role_Name { get; set; }
    }
}
