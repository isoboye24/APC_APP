using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IAttendanceStatusService
    {
        List<AttendanceStatus> GetAll();
    }
}
