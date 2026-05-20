using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IEventReceiptService
    {
        List<EventReceiptDTO> GetAll(int eventId);
        List<EventReceiptDTO> GetAllDeletedEventReceipts();
        bool Create(EventReceipt data);
        bool Update(EventReceipt data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
