using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class PersonalAttendanceDetailDTO
    {
        public int AttendanceID { get; set; }
        public int AttendanceStatusID { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public int MemberID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int GenderID { get; set; }
        public string Gender { get; set; }
        public string AttendanceStatusName { get; set; }
        public decimal MonthlyDue { get; set; }
        public decimal ExpectedDue { get; set; }
        public decimal Balance { get; set; }
        public int GeneralAttendanceID { get; set; }
    }
}
