using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Services.Interface
{
    public interface ITicketService
    {
        public List<Ticket> GetAllTickets();
        public Ticket GetTicketById(Guid? id);
        public Ticket CreateNewTicket(Ticket newEntity);
        public Ticket UpdateExistingTicket(Ticket updatedTicket);
        public Ticket DeleteTicketById(Guid? id);
        public bool TicketExists(Guid? id);
        public IEnumerable<Ticket> GetTicketsByEmail(string userEmail);
    }
}
