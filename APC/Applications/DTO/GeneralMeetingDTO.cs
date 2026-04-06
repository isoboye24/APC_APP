using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class GeneralMeetingDTO
    {
        public int GeneralMeetingId { get; set; }
        public int TotalMembersPresent { get; set; }
        public int TotalMembersAbsent { get; set; }
        public decimal TotalDuesPaid { get; set; }
        public string FormattedTotalDuesPaid { get; set; }
        public decimal TotalDuesExpected { get; set; }
        public string FormattedTotalDuesExpected { get; set; }
        public string TotalDuesBalance { get; set; }
        public string FinesRaised { get; set; }
        public string Summary { get; set; }
        public DateTime GeneralMeetingDate { get; set; }
        public string MonthName { get; set; }
        public int MonthId { get; set; }
        public int Year { get; set; }
    }
}
