using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IEmploymentStatusRepository
    {
        IQueryable<EMPLOYMENT_STATUS> GetAll();
        IQueryable<EMPLOYMENT_STATUS> GetAllEmploymentStatuses();
        EmploymentStatus GetById(int id);
        bool Insert(EmploymentStatus status);
        bool Update(EmploymentStatus status);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
