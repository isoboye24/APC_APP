using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IAttendanceStatusRepository
    {
        List<AttendanceStatus> GetAll();
        AttendanceStatus GetById(int id);
    }
}
