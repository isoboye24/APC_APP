using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IMemberService
    {
        AuthenticationDTO Authenticate(string username, string password);

        List<MemberFullDetailsDTO> GetAll();
        List<MemberFullDetailsDTO> GetAllCurrentMembers();
        List<MemberFullDetailsDTO> SelectSpecificMember(int id);
        MemberFullDetailsDTO GetMemberById(int id);
        List<MemberFullDetailsDTO> GetAllDeletedMembers();
        List<MemberFullDetailsDTO> GetBirthdayMembers(int month);
        List<MemberFullDetailsDTO> GetInactiveMembers();
        List<MemberFullDetailsDTO> GetFormerMembers();
        List<DeadMemberShortDetailDTO> GetDeceasedMembers();
        List<MemberFullDetailsDTO> Get3MonthsAbsentes();

        bool Create(Member data);
        bool Update(Member data);
        bool Delete(int id);        
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
        int GetAllCurrentMembersCount();

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

        int SelectPermittedMembersCount();
        string GetLastMemberUsername();
    }
}
