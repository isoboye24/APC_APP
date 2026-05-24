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
    }
}
