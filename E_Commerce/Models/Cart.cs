using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    [Table("cart")]
    public class Cart
    {
        [Key]
        public int Cart_Id { get; set; }
        [Required(ErrorMessage ="Quantity is required")]
        public int Quantity { get; set; }
        //public int Price { get; set; }
        [Required(ErrorMessage = "Total is required")]
        public int Total { get; set; }
        public int Price { get; set; }

        [Display(Name = "product")]
        public int Product_Id { get; set; }
        [Display(Name = "user")]
        public int User_Id { get; set; }
        [NotMapped]
        public string? Product_Name { get; set; }
        [NotMapped]
        public string? Product_Desc { get; set; }
        [NotMapped]
        public string? Product_Img { get; set; }
    }
}
