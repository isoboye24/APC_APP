using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IMemberService
    {
        AuthenticationDTO Authenticate(string username, string password);

        List<MemberFullDetailsDTO> GetAll();
        List<MemberFullDetailsDTO> SelectSpecificMember(int id);
        List<MemberFullDetailsDTO> GetAllDeletedMembers();
        List<BirthdayMembersDTO> GetBirthdayMembers(int month);
        List<MembersBasicDetailDTO> GetInactiveMembers();
        List<MembersBasicDetailDTO> GetFormerMembers();
        List<DeadMemberShortDetailDTO> GetDeceasedMembers();

        bool Create(Member data);
        bool Update(Member data);
        bool Delete(int id);        
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        int Get3MonthsAbsentesCount();
        int GetUniqueProfessionCount();
        int GetUniquePositionCount();
        int GetUniqueNationalityCount();
        int GetUniquePermissionCount();

        bool DeleteMemberPermission(int id);
        int GetCurrentMaleCount();
        int GetCurrentFemaleCount();
        int GetCurrentDivisorCount();
        int GetFormerMaleCount();
        int GetFormerFemaleCount();
        int GetFormerDivisorCount();
        int GetDeceasedMaleCount();
        int GetDeceasedFemaleCount();
        int GetDeceasedDivisorCount();

        int GetPresentMembersCount(int ID);
        int GetAbsentMembersCount(int ID);
        decimal GetAmountContributed(int ID);
        decimal GetAmountExpected(int ID);


        int SelectPermittedMembersCount();
    }
}
