using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IAttendanceStatusRepository
    {
        IQueryable<ATTENDANCE_STATUS> GetAll();
        IQueryable<ATTENDANCE_STATUS> GetById(int id);
    }
}
