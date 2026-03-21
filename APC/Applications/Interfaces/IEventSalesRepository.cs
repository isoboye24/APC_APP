using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IEventSalesRepository
    {
        List<EventSales> GetAll();
        EventSales GetById(int id);
        bool Insert(EventSales data);
        bool Update(EventSales data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int eventId, decimal amountSold, string summary, DateTime salesDate);
        int Count();
    }
}
