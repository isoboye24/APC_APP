using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IEventsRepository
    {
        IQueryable<EVENTS> GetAll();
        IQueryable<EVENTS> GetAllDeletedEvents();
        IQueryable<EVENTS> GetById(int id);
        bool Insert(TheEvents data);
        bool Update(TheEvents data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string title, DateTime eventsDate);
        int Count();
    }
}
