using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid? id);
        T Insert(T entity);
        T Update(T entity);
        T Delete(T entity);
        public IEnumerable<Ticket> GetTicketsByEmail(string userEmail);
    }
}
