using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IEventExpenditureRepository
    {
        IQueryable<EVENT_EXPENDITURE> GetAll(int eventId);
        IQueryable<EVENT_EXPENDITURE> GetAllDeletedEventExpenditures();
        IQueryable<EVENT_EXPENDITURE> GetById(int id);
        bool Insert(EventExpenditure data);
        bool Update(EventExpenditure data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int eventId, decimal spentAmount, string summary, DateTime date);
        int Count();
    }
}
