using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IFinancialReportService
    {
        List<FinancialReportDTO> GetAll();
        bool Create(FinancialReport data);
        bool Update(FinancialReport data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        decimal GetOverallTotalDues();
        decimal GetTotalDuesByMonth(int month, int year);
        decimal GetTotalDuesByYear(int year);

        decimal GetOverallExpenditures();
        decimal GetOverallExpendituresByYear(int year);
    }
}
