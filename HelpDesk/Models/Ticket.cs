using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Ticket
    {

        [Display(Name = "Ticket ID")]
        public int TicketId { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required(ErrorMessage = "Please specify what your problem is about.")]
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;

        [Required(ErrorMessage = "The problem summary cannot be left blank.")]
        [Display(Name = "Problem Summary")]
        public string? ProblemSummary { get; set; } = null!;

        [Required(ErrorMessage = "Description field cannot be left blank.")]
        [Display(Name = "Problem Description")]
        public string? Problem { get; set; } = null!;
        public string? TicketFile { get; set; } = string.Empty;

        [Display(Name = "Ticket Date")]
        public DateTime TicketDate { get; set; }
        public string? AssignedUserId { get; set; } 
        public AppUser? AssignedUser { get; set; }
    }
}