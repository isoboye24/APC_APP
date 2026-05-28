using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IEventReceiptService
    {
        List<EventReceiptDTO> GetAll();
        List<EventReceiptDTO> GetByEvent(int eventId);
        List<EventReceiptDTO> GetAllDeletedEventReceipts();
        bool Create(EventReceipt data);
        bool Update(EventReceipt data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        int GetEventReceiptsCountByEvent(int eventId);
        int GetAllEventReceiptsCount();
    }
}
