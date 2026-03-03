using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
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

        public List<AttendanceStatus> GetAll()
        {
            var data = _db.ATTENDANCE_STATUS
                .OrderBy(x => x.attendanceStatusID)
                .ToList();

            return data
                .Select(x => AttendanceStatus.Rehydrate(
                    x.attendanceStatusID,
                    x.attendanceStatus
                ))
                .ToList();
        }

        public AttendanceStatus GetById(int id)
        {
            var entity = _db.ATTENDANCE_STATUS.FirstOrDefault(x => x.attendanceStatusID == id);
            if (entity == null) return null;

            var status = new AttendanceStatus(entity.attendanceStatus);
            status.SetId(entity.attendanceStatusID);
            return status;
        }
    }
}
