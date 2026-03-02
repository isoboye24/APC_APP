using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IEmploymentStatusService
    {
        List<EmploymentStatus> GetAll();
        bool Create(EmploymentStatus employmentStatus);
        bool Update(EmploymentStatus employmentStatus);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
