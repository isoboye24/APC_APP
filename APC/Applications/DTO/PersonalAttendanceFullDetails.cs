using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class PersonalAttendanceFullDetails
    {
        public int PersonalAttendanceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string AttendanceStatus { get; set; }
        public decimal? DuesPaid { get; set; }
        public string Gender { get; set; }
    }
}
