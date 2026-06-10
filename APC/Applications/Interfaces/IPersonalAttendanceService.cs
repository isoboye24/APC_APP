using APC.Applications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IPersonalAttendanceService
    {
        int GetTotalMembersPresentCountById(int memberId);
        int GetAnnualMembersPresentCountById(int memberId, int year);
        int GetTotalMembersAbsentCountById(int memberId);
        int GetAnnualMembersAbsentCountById(int memberId, int year);

        List<PersonalAttendanceDetailsDTO> GetTotalGeneralMeetingAttendanceById(int memberId);
        List<PersonalAttendanceDetailsDTO> GetAnnualGeneralMeetingAttendanceById(int memberId, int year);

        List<YearDTO> GetPersonalAttendanceYears(int memberId);
    }
}
