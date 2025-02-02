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
        public decimal TotalAmountSpent { get; set; }
        public decimal Balance { get; set; }
        public string Summary { get; set; }
    }
}
