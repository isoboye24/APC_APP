using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IMemberService
    {
        List<Member> GetAll();
        bool Create(Member data);
        bool Update(Member data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
        AuthenticationDTO Authenticate(string username, string password);

        List<Member> GetAllDeleted();
        List<MemberFullDetailsDTO> GetFullMemberDetails();
        List<BirthdayMembersDTO> GetBirthdayMembers(int month);
        List<MembersBasicDetailDTO> GetInactiveMembers();
        List<MembersBasicDetailDTO> GetFormerMembers();
        List<DeadMemberShortDetailDTO> GetDeceasedMembers();
    }
}
