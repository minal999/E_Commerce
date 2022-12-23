using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace E_Commerce.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Product_Id { get; set; }
        [Required(ErrorMessage ="Product name is required")]
        [Display(Name = "Product Name")]
        public string? Product_Name { get; set; }
        [Required(ErrorMessage = "Product Discription is required")]
        [MaxLength(8000)]
        [Display(Name = "Procuct Description")]
        public string Product_Desc { get; set; }
        [Required(ErrorMessage = "Product price is required")]
        [Display(Name = "Price")]
        public int Price { get; set; }
    
        [Required(ErrorMessage = "Product image is required")]
        [Display(Name = "Product Image")]
       // [Required(ErrorMessage = "Only Image files allowed.")]
        public string Product_Img { get; set; }

        [Display(Name = "category")]
        public int Category_Id { get; set; }
        [NotMapped]
        public string Category_Name { get; set; }

     
    }
}
