using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace E_Commerce.Models
{
    [Table("product_category")]
    public class Category
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Category_Id { get; set; }
        [Required(ErrorMessage ="Category is reuired")]
        public string? Category_Name { get; set; }

    }
}
