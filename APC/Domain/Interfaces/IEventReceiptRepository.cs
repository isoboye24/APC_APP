using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IEventReceiptRepository
    {
        List<EventReceipt> GetAll();
        EventReceipt GetById(int id);
        bool Insert(EventReceipt data);
        bool Update(EventReceipt data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int eventId, string imagePath);
        int Count();
    }
}
