using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Services.Interface
{
    public interface IConcertService
    {
        public List<Concert> GetAllConcerts();
        public Concert GetConcertById(Guid? id);
        public Concert CreateNewConcert(Concert newEntity);
        public Concert UpdateExistingConcert(Concert updatedConcert);
        public Concert DeleteConcertById(Guid? id);
        public bool ConcertExists(Guid? id);
    }
}
