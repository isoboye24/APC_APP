using APC.Applications.DTO;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IAttendanceStatusService
    {
        List<AttendanceStatusDTO> GetAll();
        List<AttendanceStatusDTO> GetById(int id);
    }
}
