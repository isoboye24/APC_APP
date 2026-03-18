using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IMaritalStatusService
    {
        List<MaritalStatusDTO> GetAll();
        bool Create(MaritalStatus maritalStatus);
        bool Update(MaritalStatus maritalStatus);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
