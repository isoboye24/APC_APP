using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IFinedMemberRepository
    {
        List<FinedMember> GetAll();
        FinedMember GetById(int id);
        bool Insert(FinedMember data);
        bool Update(FinedMember data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int constitutionId, int memberId, DateTime fineDate);
        int Count();
    }
}
