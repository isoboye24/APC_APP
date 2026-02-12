using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class GeneralAttendanceDTO
    {
        public List<GeneralAttendanceDetailDTO> GeneralAttendance { get; set; }
        public List<MonthDetailDTO> Months { get; set; }
        public List<int> Years { get; set; }
    }
}
