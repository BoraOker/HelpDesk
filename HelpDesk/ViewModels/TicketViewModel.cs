using System.ComponentModel.DataAnnotations;
using HelpDesk.Models;
namespace HelpDesk.ViewModels
{
    public class TicketViewModel {
        public int TicketId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProblemSummary { get; set; } = null!;
        public string Problem { get; set; } = null!;
        public DateTime TicketDate { get; set; }
        public string? TicketFile { get; set; }
        public string? AssignedUserId { get; set; } 
        public AppUser? AssignedUser { get; set; }
    }
}