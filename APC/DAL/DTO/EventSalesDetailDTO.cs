using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class EventSalesDetailDTO
    {
        public int EventSalesID { get; set; }
        public int EventID { get; set; }
        public string Summary { get; set; }
        public decimal AmountSold { get; set; }
        public DateTime SalesDate { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}
