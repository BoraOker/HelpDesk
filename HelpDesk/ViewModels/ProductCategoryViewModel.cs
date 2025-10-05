using System.ComponentModel;
using HelpDesk.Models;
namespace HelpDesk.ViewModels
{
    public class ProductCategoryViewModel {
        public int ProductCategoryId { get; set; }
        public string? CategoryContext { get; set; }
        public int ProductId { get; set; }
    }
}