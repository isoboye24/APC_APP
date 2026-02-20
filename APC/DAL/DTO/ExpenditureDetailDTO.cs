using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class ExpenditureDetailDTO
    {
        public int ExpenditureID { get; set; }
        public string Summary { get; set; }
        public decimal AmountSpent { get; set; }
        public string AmountSpentWithCurrency { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime ExpenditureDate { get; set; }
    }
}
