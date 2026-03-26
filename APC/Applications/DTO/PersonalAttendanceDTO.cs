using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class PersonalAttendanceDTO
    {
        public int Counter { get; set; }
        public int PersonalAttendanceId { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AttendanceStatusId { get; set; }
        public string AttendanceStatus { get; set; }
        public decimal DuesPaid { get; set; }
        public string Gender { get; set; }
        public int GeneralMeetingId { get; set; }
    }
}
