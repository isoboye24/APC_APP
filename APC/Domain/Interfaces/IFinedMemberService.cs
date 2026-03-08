using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IFinedMemberService
    {
        List<FinedMember> GetAll();
        bool Create(FinedMember data);
        bool Update(FinedMember data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
