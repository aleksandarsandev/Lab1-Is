using TicketShop.Domain.Identity;

namespace TicketShop.Domain.DomainModels
{
    public class Ticket:BaseEntity
    {
        public int NumberOfPeople { get; set; }
        public string? TicketUserId { get; set; }
        public virtual TicketUser? TicketUser { get; set; }
        public Guid? ConcertId { get; set; }
        public virtual Concert? Concert { get;}
        public double TotalPrice { get; set; }
    }
}
