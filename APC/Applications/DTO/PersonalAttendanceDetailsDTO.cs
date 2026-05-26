using System;

namespace APC.Applications.DTO
{
    public class PersonalAttendanceDetailsDTO
    {
        public int MonthId { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public string AttendanceStatus { get; set; }
        public decimal MonthlyDues { get; set; }
    }
}
