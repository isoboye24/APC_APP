using APC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class PersonalAttendance
    {
        public int PersonalAttendanceId { get; private set; }
        public int AttendanceStatusId { get; private set; }
        public int MemberId { get; private set; }
        public decimal? MonthlyDues { get; private set; }
        public decimal? ExpectedDues { get; private set; }
        public decimal? Balance { get; private set; }
        public int GeneralMeetingId { get; private set; }
        

        public PersonalAttendance(int attendanceStatusId, int memberId, decimal? monthlyDues,decimal? expectedDues, decimal? balance, 
            int generalMeetingId)
        {
            SetAttendanceStatus(attendanceStatusId);
            SetMember(memberId);
            SetMonthlyDues(monthlyDues);
        }

        public static PersonalAttendance Rehydrate(
                int id,
                int attendanceStatusId, 
                int memberId, 
                decimal? monthlyDues,
                decimal? expectedDues, 
                decimal? balance, 
                int generalMeetingId
            )
        {
            var personalAttendance = new PersonalAttendance( attendanceStatusId, memberId, monthlyDues, expectedDues,balance, generalMeetingId);

            personalAttendance.PersonalAttendanceId = id;
            personalAttendance.GeneralMeetingId = generalMeetingId;
            personalAttendance.ExpectedDues = expectedDues;
            personalAttendance.Balance = balance;

            return personalAttendance;
        }

        private void SetAttendanceStatus(int statusId)
        {
            if (statusId < 0)
                throw new ArgumentException("Invalid attendance status");

            AttendanceStatusId = statusId;
        }

        public void UpdateAttendanceStatus(int newStatusId)
        {
            SetAttendanceStatus(newStatusId);
        }

        private void SetMember(int memberId)
        {
            if (memberId < 0)
                throw new ArgumentException("Invalid member");

            MemberId = memberId;
        }

        public void UpdateMembers(int newMemberId)
        {
            SetMember(newMemberId);
        }

        private void SetMonthlyDues(decimal? monthlyDues)
        {
            var value = monthlyDues ?? 0;
            MonthlyDues = value < 0 ? 0 : value;
        }

        public void UpdateMonthlyDues(decimal newMonthlyDues)
        {
            SetMonthlyDues(newMonthlyDues);
        }
    }
}
