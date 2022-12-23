using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Order_Id { get; set; }
        [Display(Name = "user")]
        public int User_Id { get; set; }
        [ForeignKey("User_Id")]
        public virtual User? Users { get; set; }
        [Required(ErrorMessage ="Total is required")]
        public int Total { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Display(Name = "product")]
        public int Product_Id { get; set; }
        [ForeignKey("Product_Id")]
        public virtual Product? Products { get; set; }
    }

}
