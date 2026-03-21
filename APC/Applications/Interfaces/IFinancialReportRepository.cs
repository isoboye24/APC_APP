using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IFinancialReportRepository
    {
        List<FinancialReport> GetAll();
        FinancialReport GetById(int id);
        bool Insert(FinancialReport data);
        bool Update(FinancialReport data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int year);
        int Count();
    }
}
