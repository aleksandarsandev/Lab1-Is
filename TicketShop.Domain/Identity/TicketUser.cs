using Microsoft.AspNetCore.Identity;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Domain.Identity
{
    public class TicketUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
