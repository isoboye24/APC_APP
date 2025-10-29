using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class SpecialContributorDetailDTO
    {
        public int ContributorID { get; set; }
        public int MemberID { get; set; }
        public int Counter { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImagePath { get; set; }
        public decimal AmountExpected { get; set; }
        public string AmountExpectedWithCurrency { get; set; }
        public decimal AmountContributed { get; set; }
        public string AmountContributedWithCurrency { get; set; }
        public string Balance { get; set; }
        public string AmountContributedStatus { get; set; }
        public DateTime ContributedDate { get; set; }
        public string Date { get; set; }
        public int ContributionID { get; set; }
        public string Summary { get; set; }
    }
}
