using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class AbsenteesDTO
    {
        public int MemberID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string PositionName { get; set; }
        public string AttendanceStatus { get; set; }
        public string GenderName { get; set; }
        public string NoOfAbsent { get; set; }
    }
}
