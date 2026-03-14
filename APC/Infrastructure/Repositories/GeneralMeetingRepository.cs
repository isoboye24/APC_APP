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

        public bool Exists(int month, int year)
        {
            return _db.GENERAL_ATTENDANCE.Any(x => !x.isDeleted && x.attendanceDate.Month == month && x.attendanceDate.Year == year);
        }

        public List<GeneralMeeting> GetAll()
        {
            var data = _db.GENERAL_ATTENDANCE
                .Where(x => !x.isDeleted)
                .ToList();

            return data
                .Select(x => GeneralMeeting.Rehydrate(
                    x.generalAttendanceID,
                    x.totalMembersPresent,
                    x.totalMembersAbsent,
                    x.totalDuesPaid,
                    x.totalDuesExpected,
                    x.totalDuesBalance,
                    x.summary,
                    x.attendanceDate
                ))
                .ToList();
        }

        public List<GeneralMeeting> GetAllDeleted()
        {
            var data = _db.GENERAL_ATTENDANCE
                .Where(x => x.isDeleted)
                .ToList();

            return data
                .Select(x => GeneralMeeting.Rehydrate(
                    x.generalAttendanceID,
                    x.totalMembersPresent,
                    x.totalMembersAbsent,
                    x.totalDuesPaid,
                    x.totalDuesExpected,
                    x.totalDuesBalance,
                    x.summary,
                    x.attendanceDate
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.GENERAL_ATTENDANCE.First(x => x.generalAttendanceID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public GeneralMeeting GetById(int id)
        {
            var entity = _db.GENERAL_ATTENDANCE
                .Where(x => x.generalAttendanceID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.generalAttendanceID,
                    x.totalMembersPresent,
                    x.totalMembersAbsent,
                    x.totalDuesPaid,
                    x.totalDuesExpected,
                    x.totalDuesBalance,
                    x.summary,
                    x.attendanceDate
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return GeneralMeeting.Rehydrate(
                    entity.generalAttendanceID,
                    entity.totalMembersPresent,
                    entity.totalMembersAbsent,
                    entity.totalDuesPaid,
                    entity.totalDuesExpected,
                    entity.totalDuesBalance,
                    entity.summary,
                    entity.attendanceDate
            );
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
