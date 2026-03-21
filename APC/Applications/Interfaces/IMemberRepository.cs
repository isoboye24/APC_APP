using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
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
        MEMBER GetByUsername(string username);
        int Count();
        List<MembersBasicDetailDTO> GetAllDeletedMembers();
        List<MemberFullDetailsDTO> GetFullMemberDetails();
        List<BirthdayMembersDTO> GetBirthdayMembers(int month);
        List<MembersBasicDetailDTO> GetInactiveMembers();
        List<MembersBasicDetailDTO> GetFormerMembers();
        List<DeadMemberShortDetailDTO> GetDeceasedMembers();

        int Get3MonthsAbsentesCount();
        int GetUniqueProfessionCount();
        int GetUniquePositionCount();
        int GetUniqueNationalityCount();
        int GetUniquePermissionCount();
    }
}
