using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Infrastructure.Repositories
{
    public class AttendanceStatusRepository : IAttendanceStatusRepository
    {
        private readonly APCEntities _db;
        public AttendanceStatusRepository(APCEntities db)
        {
            _db = db;
        }

        public IQueryable<ATTENDANCE_STATUS> GetAll()
        {
            return _db.ATTENDANCE_STATUS.Where(x => !x.isDeleted);
        }

        public IQueryable<ATTENDANCE_STATUS> GetById(int id)
        {
            return _db.ATTENDANCE_STATUS.Where(x => !x.isDeleted && x.attendanceStatusID == id);
        }

        public IQueryable<ATTENDANCE_STATUS> GetByStatus(string status)
        {
            return _db.ATTENDANCE_STATUS.Where(x => !x.isDeleted && x.attendanceStatus == status);
        }
    }
}
