using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IExpenditureService
    {
        List<ExpenditureDTO> GetAll();
        List<ExpenditureDTO> GetAllDeletedExpenditures();
        List<ExpenditureDTO> GetAnnualExpenditures(int year);
        List<YearDTO> GetExpenditureYearsOnly();
        bool Create(Expenditure data);
        bool Update(Expenditure data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
