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
    public class ConcertService : IConcertService
    {
        private readonly IRepository<Concert> _concertRepository;

        public ConcertService(IRepository<Concert> concertRepository)
        {
            _concertRepository = concertRepository;
        }

        public List<Concert> GetAllConcerts()
        {
            return _concertRepository.GetAll().ToList();
        }

        public Concert GetConcertById(Guid? id)
        {
            return _concertRepository.GetById(id);
        }

        public Concert CreateNewConcert(Concert newEntity)
        {
            return _concertRepository.Insert(newEntity);
        }

        public Concert UpdateExistingConcert(Concert updatedConcert)
        {
            return _concertRepository.Update(updatedConcert);
        }

        public Concert DeleteConcertById(Guid? id)
        {
            return _concertRepository.Delete(GetConcertById(id));
        }

        public bool ConcertExists(Guid? id)
        {
            return _concertRepository.GetById(id) != null;
        }
    }
}
