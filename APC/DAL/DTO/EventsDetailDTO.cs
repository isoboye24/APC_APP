using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class EventsDetailDTO
    {
        public int EventID { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public string EventTitle { get; set; }
        public string Summary { get; set; }
        public string CoverImagePath { get; set; }
        public DateTime EventDate { get; set; }
    }
}
