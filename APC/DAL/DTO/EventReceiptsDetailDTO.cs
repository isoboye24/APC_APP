using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class EventReceiptsDetailDTO
    {
        public int EventReceiptID { get; set; }
        public int EventID { get; set; }
        public string Summary { get; set; }
        public string ImagePath { get; set; }
        public int Counter { get; set; }
        public string ImageCaption { get; set; }
        public decimal AmountSpent { get; set; }
        public string AmountSpentWithCurrency { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public DateTime ReceiptDate { get; set; }
    }
}
