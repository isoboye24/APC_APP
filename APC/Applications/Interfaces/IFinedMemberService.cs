using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IFinedMemberService
    {
        List<FinedMemberDTO> GetAll();
        List<FinedMemberDTO> GetAllDeletedFinedMembers();
        bool Create(FinedMember data);
        bool Update(FinedMember data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
