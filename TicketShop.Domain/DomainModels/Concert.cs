namespace TicketShop.Domain.DomainModels
{
    public class Concert:BaseEntity
    {
        public string ConcertName { get; set; }
        public string ConcertPlace { get; set; }
        public DateTime ConcertDate { get; set; }
        public double ConcertPrice { get; set; }
       // public virtual ICollection<ConcertOnTicket> ConcertOnTickets { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
}
