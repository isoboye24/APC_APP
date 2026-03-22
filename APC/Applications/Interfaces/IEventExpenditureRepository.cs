using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IEventExpenditureRepository
    {
        List<EventExpenditure> GetAll();
        EventExpenditure GetById(int id);
        bool Insert(EventExpenditure data);
        bool Update(EventExpenditure data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int eventId, decimal spentAmount, string summary, DateTime date);
        int Count();
    }
}
