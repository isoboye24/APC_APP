using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class EventExpenditureDetailDTO
    {
        public int EventExpenditureID { get; set; }
        public int EventID { get; set; }
        public string Summary { get; set; }
        public decimal AmountSpent { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}
