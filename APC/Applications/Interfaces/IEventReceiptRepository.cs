using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IEventReceiptRepository
    {
        IQueryable<EVENT_RECEIPTS> GetAll(int eventId);
        IQueryable<EVENT_RECEIPTS> GetAllDeletedEventReceipts();
        IQueryable<EVENT_RECEIPTS> GetById(int id);
        bool Insert(EventReceipt data);
        bool Update(EventReceipt data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int eventId, string imagePath);
        int Count();
    }
}
