using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class GeneralAttendanceDetailDTO
    {
        public int GeneralAttendanceID { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int TotalMembersPresent { get; set; }
        public int TotalMembersAbsent { get; set; }
        public decimal TotalDuesPaid { get; set; }
        public string TotalDuesPaidWithCurrency { get; set; }
        public decimal TotalDuesExpected { get; set; }
        public string TotalDuesExpectedWithCurrency { get; set; }
        public decimal TotalDuesBalance { get; set; }
        public string TotalDuesBalanceWithCurrency { get; set; }
        public string Summary { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string FinancialStatus { get; set; }
    }
}
