using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IMaritalStatusService
    {
        List<MaritalStatus> GetAll();
        bool Create(MaritalStatus maritalStatus);
        bool Update(MaritalStatus maritalStatus);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
