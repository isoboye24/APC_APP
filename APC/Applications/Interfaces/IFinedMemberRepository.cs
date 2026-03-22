using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IFinedMemberRepository
    {
        IQueryable<FINED_MEMBER> GetAll();
        IQueryable<FINED_MEMBER> GetAllDeletedFinedMembers();
        IQueryable<FINED_MEMBER> GetById(int id);
        bool Insert(FinedMember data);
        bool Update(FinedMember data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int constitutionId, int memberId, DateTime fineDate);
        int Count();
    }
}
