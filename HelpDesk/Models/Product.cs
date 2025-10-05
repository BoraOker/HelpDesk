using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Product {

        [Display(Name ="Product ID")]
        public int ProductId { get; set; }

        [Required(ErrorMessage= "Product Name Field cannot be left blank.")]
        [Display(Name ="Product Name")]
        public string? ProductName { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public ICollection<FAQ> FAQs { get; set; } = new List<FAQ>();
        
    }
}