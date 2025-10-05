using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class ProductCategory {
        [Display(Name ="Product Category ID")]
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage= "Category Content Field cannot be left blank.")]
        [Display(Name ="Category Context")]
        public string? CategoryContext { get; set; }

        [Display(Name ="Product Name")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}