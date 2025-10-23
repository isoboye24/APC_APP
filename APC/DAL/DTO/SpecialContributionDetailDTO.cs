using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class SpecialContributionDetailDTO
    {
        public int SpecialContributionID { get; set; }
        public string ContributionTitle { get; set; }
        public string Summary { get; set; }
        public  decimal AmountToContribute { get; set; }
        public  string AmountToContributeWithCurrency { get; set; }
        public  decimal AmountExpected { get; set; }
        public  string AmountExpectedWithCurrency { get; set; }
        public  decimal AmountContributed { get; set; }
        public  string AmountContributedWithCurrency { get; set; }
        public string Status { get; set; }
        public int SupervisorID { get; set; }
        public string SupervisorName { get; set; }
        public int Members { get; set; }
        public  DateTime ContributionStartDate { get; set; }
        public string StartDate { get; set; }
        public  DateTime ContributionEndDate { get; set; }
        public string EndDate { get; set; }
    }
}
