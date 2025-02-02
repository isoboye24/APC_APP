using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class PersonalAttendanceDTO
    {
        public List<MemberDetailDTO> Members { get; set; }
        public List<MemberDetailDTO> Members3MonthsAbsentees { get; set; }
        public List<MonthDetailDTO> Months { get; set; }
        public List<AttendanceStatusDetailDTO> AttendanceStatuses { get; set; }
        public List<GenderDetailDTO> Genders { get; set; }
        public List<PersonalAttendanceDetailDTO> PresentMember { get; set; }
        public List<PersonalAttendanceDetailDTO> AbsentMember { get; set; }
        public List<PersonalAttendanceDetailDTO> PersonalAttendances { get; set; }
        public List<PersonalAttendanceDetailDTO> AmountsContributed { get; set; }
        public List<PersonalAttendanceDetailDTO> AmountExpected { get; set; }
        public List<PersonalAttendanceDetailDTO> AmountsBalance { get; set; }
        public List<GeneralAttendanceDetailDTO> GeneralAttendance { get; set; }
        public List<FinedMemberDetailDTO> FinedMember { get; set; }
        public List<int> Years { get; set; }
    }
}
