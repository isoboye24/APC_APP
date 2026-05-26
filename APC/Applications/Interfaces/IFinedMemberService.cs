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

        decimal GetTotalPaidFines();
        decimal GetTotalFinesExpected();

        decimal GetTotalFinesPaidByMember(int memberId);
        decimal GetTotalFinesExpectedByMember(int memberId);
        int AnnualFinesCountById(int memberId, int year);
        int TotalFinesCountById(int memberId);

        List<FinedMemberDTO> GetAllFineListsById(int memberId);
        List<FinedMemberDTO> GetAnnualFineListsById(int memberId, int year);
    }
}
