using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class PersonalAttendanceRepository : IPersonalAttendanceRepository
    {
        private readonly APCEntities _db;
        public PersonalAttendanceRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.PERSONAL_ATTENDANCE.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.PERSONAL_ATTENDANCE.First(x => x.attendanceID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(int memberId, int generalMeetingId)
        {
            return _db.PERSONAL_ATTENDANCE.Any(x => !x.isDeleted && x.memberID == memberId && x.generalAttendanceID == generalMeetingId);
        }

        public IQueryable<PERSONAL_ATTENDANCE> GetAllByGeneralMeetingId(int id)
        {
            return _db.PERSONAL_ATTENDANCE.Where(x => !x.isDeleted && x.generalAttendanceID == id);
        }
        
        public IQueryable<PERSONAL_ATTENDANCE> GetAllDeletedPersonalAttendance()
        {
            return _db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.PERSONAL_ATTENDANCE.First(x => x.attendanceID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<PERSONAL_ATTENDANCE> GetById(int id)
        {
            return _db.PERSONAL_ATTENDANCE.Where(x => !x.isDeleted && x.attendanceID == id);
        }

        public bool Insert(PersonalAttendance data)
        {
            _db.PERSONAL_ATTENDANCE.Add(new PERSONAL_ATTENDANCE
            {
                attendanceStatusID = data.AttendanceStatusId,
                memberID = data.MemberId,
                monthlyDues = data.MonthlyDues,
                expectedMonthlyDue = data.ExpectedDues,
                balance = data.Balance,
                generalAttendanceID = data.GeneralMeetingId,

                day = DateTime.Today.Day,
                monthID = DateTime.Today.Month,
                year = DateTime.Today.Year,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.PERSONAL_ATTENDANCE.FirstOrDefault(x => x.attendanceID == id);

            if (entity == null)
                return false;

            _db.PERSONAL_ATTENDANCE.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(PersonalAttendance data)
        {
            var entity = _db.PERSONAL_ATTENDANCE.First(x => x.attendanceID == data.PersonalAttendanceId);
            entity.attendanceStatusID = data.AttendanceStatusId;
            entity.memberID = data.MemberId;
            entity.monthlyDues = data.MonthlyDues;
            entity.expectedMonthlyDue = data.ExpectedDues;
            entity.balance = data.Balance;
            entity.generalAttendanceID = data.GeneralMeetingId;

            _db.SaveChanges();
            return true;
        }
    }
}
