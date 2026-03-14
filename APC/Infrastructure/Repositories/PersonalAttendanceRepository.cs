using APC.Applications.DTO;
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

        public List<PersonalAttendance> GetAll()
        {
            var data = _db.PERSONAL_ATTENDANCE
                .Where(x => !x.isDeleted)
                .ToList();

            return data
                .Select(x => PersonalAttendance.Rehydrate(
                    x.attendanceID,
                    x.attendanceStatusID,
                    x.memberID,
                    x.monthlyDues,
                    x.expectedMonthlyDue,
                    x.balance,
                    x.generalAttendanceID
                ))
                .ToList();
        }

        public List<PersonalAttendance> GetAllDeleted()
        {
            var data = _db.PERSONAL_ATTENDANCE
                .Where(x => x.isDeleted)
                .ToList();

            return data
                .Select(x => PersonalAttendance.Rehydrate(
                    x.attendanceID,
                    x.attendanceStatusID,
                    x.memberID,
                    x.monthlyDues,
                    x.expectedMonthlyDue,
                    x.balance,
                    x.generalAttendanceID
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.PERSONAL_ATTENDANCE.First(x => x.attendanceID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public PersonalAttendance GetById(int id)
        {
            var entity = _db.PERSONAL_ATTENDANCE
                .Where(x => x.attendanceID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.attendanceID,
                    x.attendanceStatusID,
                    x.memberID,
                    x.monthlyDues,
                    x.expectedMonthlyDue,
                    x.balance,
                    x.generalAttendanceID
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return PersonalAttendance.Rehydrate(
                    entity.attendanceID,
                    entity.attendanceStatusID,
                    entity.memberID,
                    entity.monthlyDues,
                    entity.expectedMonthlyDue,
                    entity.balance,
                    entity.generalAttendanceID
            );
        }

        public List<PersonalAttendanceFullDetails> GetFullPersonalAttendanceDetails()
        {
            var member = (from p in _db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false)
                          join m in _db.MEMBER on p.memberID equals m.memberID
                          join g in _db.GENDER on m.genderID equals g.genderID
                          join ats in _db.ATTENDANCE_STATUS on p.attendanceStatusID equals ats.attendanceStatusID
                          select new PersonalAttendanceFullDetails
                          {
                              PersonalAttendanceId = p.attendanceID,
                              FirstName = m.name,
                              LastName = m.surname,
                              ImagePath = m.imagePath,
                              AttendanceStatus = ats.attendanceStatus,
                              DuesPaid = p.monthlyDues,
                              Gender = g.genderName,
                          });

            return member.ToList();
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
