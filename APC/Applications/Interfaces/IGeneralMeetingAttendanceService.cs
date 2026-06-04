using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IGeneralMeetingAttendanceService
    {
        List<GeneralMeetingAttendanceDTO> GetAllByGeneralMeetingId(int id);
        List<GeneralMeetingAttendanceDTO> GetMemberPersonalAttendanceByYear(int memberId, int year);
        List<GeneralMeetingAttendanceDTO> GetAllDeletedPersonalAttendance();
        GeneralMeetingAttendanceDTO GetPersonalAttendanceById(int memberId, int generalMeetingId);

        bool Create(PersonalAttendance data);
        bool Update(PersonalAttendance data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
        int GetMembersPresentCount(int generalMeetingId);
        int GetMembersAbsentCount(int generalMeetingId);
        decimal GetTotalDuesPaid(int generalMeetingId);
        int GetLastMeetingPresentMembersCount();

    }
}
