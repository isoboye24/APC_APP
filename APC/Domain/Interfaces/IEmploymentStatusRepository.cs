using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IEmploymentStatusRepository
    {
        List<EmploymentStatus> GetAll();
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
