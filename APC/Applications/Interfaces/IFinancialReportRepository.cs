using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IFinancialReportRepository
    {
        IQueryable<FINANCIAL_REPORT> GetAll();
        IQueryable<FINANCIAL_REPORT> GetAllDeletedFinancialReports();
        IQueryable<FINANCIAL_REPORT> GetById(int id);
        bool Insert(FinancialReport data);
        bool Update(FinancialReport data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int year);
        int Count();
    }
}
