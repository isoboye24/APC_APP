using System;
using static OfficeOpenXml.ExcelErrorValue;

namespace APC.Domain.Entities
{
    public class PersonalAttendance
    {
        public int PersonalAttendanceId { get; private set; }
        public int AttendanceStatusId { get; private set; }
        public int MemberId { get; private set; }
        public decimal MonthlyDues { get; private set; }
        public decimal ExpectedDues { get; private set; }
        public decimal Balance { get; private set; }
        public int GeneralMeetingId { get; private set; }
        public int Day { get; private set; }
        public int MonthId { get; private set; }
        public int Year { get; private set; }
        

        public PersonalAttendance(int attendanceStatusId, int memberId, decimal monthlyDues, decimal expectedDues, decimal balance, 
            int generalMeetingId, DateTime date)
        {
            SetAttendanceStatus(attendanceStatusId);
            SetMember(memberId);
            SetMonthlyDues(monthlyDues);
            SetExpectedDues(expectedDues);
            SetBalance(balance);
            SetPersonalAttendanceDate(date);
            SetGeneralMeetingId(generalMeetingId);
        }

        public static PersonalAttendance Rehydrate(
                int id,
                int attendanceStatusId, 
                int memberId, 
                decimal monthlyDues,
                decimal expectedDues, 
                decimal balance, 
                int generalMeetingId,
                DateTime date
            )
        {
            var personalAttendance = new PersonalAttendance( attendanceStatusId, memberId, monthlyDues, expectedDues,balance, generalMeetingId, date);

            personalAttendance.PersonalAttendanceId = id;
            personalAttendance.GeneralMeetingId = generalMeetingId;
            personalAttendance.ExpectedDues = expectedDues;
            personalAttendance.Balance = balance;

            return personalAttendance;
        }

        private void SetPersonalAttendanceDate(DateTime date)
        {
            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            Day = date.Day;
            MonthId = date.Month;
            Year = date.Year;
        }

        private void SetAttendanceStatus(int statusId)
        {
            if (statusId < 0)
                throw new ArgumentException("Invalid attendance status");

            AttendanceStatusId = statusId;
        }
        private void SetGeneralMeetingId(int meetingId)
        {
            if (meetingId < 1)
                throw new ArgumentException("Invalid attendance status");

            GeneralMeetingId = meetingId;
        }

        private void SetMember(int memberId)
        {
            if (memberId < 0)
                throw new ArgumentException("Invalid member");

            MemberId = memberId;
        }

        private void SetMonthlyDues(decimal monthlyDues)
        {
            MonthlyDues = monthlyDues > 0 ? monthlyDues : 0;
        }
        private void SetExpectedDues(decimal expectedDues)
        {
            ExpectedDues = expectedDues > 0 ? expectedDues : 0;
        }
        private void SetBalance(decimal balance)
        {
            Balance = balance > 0 ? balance : 0;
        }

    }
}
