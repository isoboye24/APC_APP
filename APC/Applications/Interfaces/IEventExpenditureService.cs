using APC.Domain.Entities;
using System;
using System.Collections.Generic;


namespace APC.Applications.Interfaces
{
    public interface IEventExpenditureService
    {
        List<EventExpenditure> GetAll();
        bool Create(EventExpenditure data);
        bool Update(EventExpenditure data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
