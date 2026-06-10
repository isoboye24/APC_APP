using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IEventSalesService
    {
        List<EventSalesDTO> GetAll();
        List<EventSalesDTO> GetAllDeletedEventSales();
        List<EventSalesDTO> GetByEvent(int eventId);
        bool Create(EventSales data);
        bool Update(EventSales data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        decimal GetSalesAmountByYear(int year);
        decimal GetSalesAmountByEvent(int eventId);
    }
}
