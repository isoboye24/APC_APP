using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IPersonalAttendanceService
    {
        List<PersonalAttendanceDTO> GetAllByGeneralMeetingId(int id);
        List<PersonalAttendanceDTO> GetAllDeletedPersonalAttendance();
        bool Create(Domain.Entities.PersonalAttendance data);
        bool Update(Domain.Entities.PersonalAttendance data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
