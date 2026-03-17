using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IEmploymentStatusService
    {
        List<EmploymentStatusDTO> GetAll();
        bool Create(EmploymentStatus employmentStatus);
        bool Update(EmploymentStatus employmentStatus);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
