using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;


namespace APC.Applications.Interfaces
{
    public interface IEventExpenditureService
    {
        List<EventExpenditureDTO> GetAll();
        List<EventExpenditureDTO> GetByEvent(int eventId);
        List<EventExpenditureDTO> GetAllDeletedEventExpenditures();
        bool Create(EventExpenditure data);
        bool Update(EventExpenditure data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        decimal GetTotalAmountSpentByEvent(int eventId);
        decimal GetTotalAmountSpentByYear(int year);
    }
}
