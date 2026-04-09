using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IExpenditureRepository
    {
        IQueryable<EXPENDITURE> GetAll();
        IQueryable<EXPENDITURE> GetAllDeletedExpenditures();
        IQueryable<EXPENDITURE> GetById(int id);
        bool Insert(Expenditure data);
        bool Update(Expenditure data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(decimal spentAmount, string summary, DateTime date);
        int Count();
    }
}
