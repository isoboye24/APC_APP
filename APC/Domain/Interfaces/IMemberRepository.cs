using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IMemberRepository
    {
        List<Member> GetAll();
        Member GetById(int id);
        bool Insert(Member data);
        bool Update(Member data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string firstName, string lastName);
        int Count();
        List<Member> GetAllDeleted();
        List<MemberFullDetailsDTO> GetFullMemberDetails();
        List<BirthdayMembersDTO> GetBirthdayMembers(int month);
        List<MembersBasicDetailDTO> GetInactiveMembers();
        List<MembersBasicDetailDTO> GetFormerMembers();
        List<DeadMemberShortDetailDTO> GetDeceasedMembers();
    }
}
