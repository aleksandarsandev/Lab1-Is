using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository.Interface;
using TicketShop.Services.Interface;

namespace TicketShop.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public TicketService(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public List<Ticket> GetAllTickets()
        {
            return _ticketRepository.GetAll().ToList();
        }

        public Ticket GetTicketById(Guid? id)
        {
            return _ticketRepository.GetById(id);
        }

        public Ticket CreateNewTicket(Ticket newEntity)
        {
            return _ticketRepository.Insert(newEntity);
        }

        public Ticket UpdateExistingTicket(Ticket updatedTicket)
        {
            return _ticketRepository.Update(updatedTicket);
        }

        public Ticket DeleteTicketById(Guid? id)
        {
            return _ticketRepository.Delete(GetTicketById(id));
        }

        public bool TicketExists(Guid? id)
        {
            return GetTicketById(id) != null;
        }

        public IEnumerable<Ticket> GetTicketsByEmail(string userEmail)
        {
            return _ticketRepository.GetTicketsByEmail(userEmail);
        }
    }
}
