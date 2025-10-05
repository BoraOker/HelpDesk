using Microsoft.AspNetCore.Identity;

namespace HelpDesk.Models
{
    public class AppUser : IdentityUser {
        public string? FullName { get; set; }

        public ICollection<Ticket> AssignedTicket { get; set; } = new List<Ticket>();
    }
}