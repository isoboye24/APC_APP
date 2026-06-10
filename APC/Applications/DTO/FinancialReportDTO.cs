using System;

namespace APC.Applications.DTO
{
    public class FinancialReportDTO
    {
        public int FinancialReportId { get; set; }
        public decimal TotalAmountRaised { get; set; }
        public string FormattedTotalAmountRaised { get; set; }
        public decimal TotalAmountSpent { get; set; }
        public string FormattedTotalAmountSpent { get; set; }
        public int Year { get; set; }
        public string Summary { get; set; }
    }
}
