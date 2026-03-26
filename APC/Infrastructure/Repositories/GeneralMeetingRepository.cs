using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class GeneralMeetingRepository : IGeneralMeetingRepository
    {
        private readonly APCEntities _db;
        public GeneralMeetingRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.GENERAL_ATTENDANCE.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.GENERAL_ATTENDANCE.First(x => x.generalAttendanceID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<GENERAL_ATTENDANCE> GetAll()
        {
            return _db.GENERAL_ATTENDANCE.Where(x => !x.isDeleted);
        }
        
        public IQueryable<GENERAL_ATTENDANCE> GetAllDeletedGeneralMeetings()
        {
            return _db.GENERAL_ATTENDANCE.Where(x => x.isDeleted);
        }

        public bool Exists(int month, int year)
        {
            return _db.GENERAL_ATTENDANCE.Any(x => !x.isDeleted && x.attendanceDate.Month == month && x.attendanceDate.Year == year);
        }

        public bool GetBack(int id)
        {
            var entity = _db.GENERAL_ATTENDANCE.First(x => x.generalAttendanceID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<GENERAL_ATTENDANCE> GetById(int id)
        {
            return _db.GENERAL_ATTENDANCE.Where(x => !x.isDeleted && x.generalAttendanceID == id);
        }

        public bool Insert(GeneralMeeting data)
        {
            _db.GENERAL_ATTENDANCE.Add(new GENERAL_ATTENDANCE
            {
                totalMembersPresent = data.TotalMembersPresent,
                totalMembersAbsent = data.TotalMembersAbsent,
                totalDuesPaid = data.TotalDuesPaid,
                totalDuesExpected = data.TotalDuesExpected,
                totalDuesBalance = data.TotalDuesBalance,
                summary = data.Summary,
                attendanceDate = data.GeneralMeetingDate,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.GENERAL_ATTENDANCE.FirstOrDefault(x => x.generalAttendanceID == id);

            if (entity == null)
                return false;

            _db.GENERAL_ATTENDANCE.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(GeneralMeeting data)
        {
            var entity = _db.GENERAL_ATTENDANCE.First(x => x.generalAttendanceID == data.GeneralMeetingId);
            entity.totalMembersPresent = data.TotalMembersPresent;
            entity.totalMembersAbsent = data.TotalMembersAbsent;
            entity.totalDuesPaid = data.TotalDuesPaid;
            entity.totalDuesExpected = data.TotalDuesExpected;
            entity.totalDuesBalance = data.TotalDuesBalance;
            entity.summary = data.Summary;
            entity.attendanceDate = data.GeneralMeetingDate;

            _db.SaveChanges();
            return true;
        }
    }
}
