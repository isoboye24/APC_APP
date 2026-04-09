using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IEventSalesRepository
    {
        IQueryable<EVENT_SALES> GetAll(int eventId);
        IQueryable<EVENT_SALES> GetAllDeletedEventSales();  
        IQueryable<EVENT_SALES> GetById(int id);
        bool Insert(EventSales data);
        bool Update(EventSales data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int eventId, decimal amountSold, string summary, DateTime salesDate);
        int Count();
    }
}
