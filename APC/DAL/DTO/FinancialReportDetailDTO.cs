using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class FinancialReportDetailDTO
    {
        public int FinancialReportID { get; set; }
        public string Year { get; set; }
        public decimal TotalAmountRaised { get; set; }
        public string TotalAmountRaisedWithCurrency { get; set; }
        public decimal TotalAmountSpent { get; set; }
        public string TotalAmountSpentWithCurrency { get; set; }
        public string Balance { get; set; }
        public string Summary { get; set; }
    }
}
