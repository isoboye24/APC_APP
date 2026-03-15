using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class SpecialContributorFullDetails
    {
        public int SpecialContributorId { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public decimal AmountContributed { get; set; }
        public decimal AmountExpected { get; set; }
        public decimal Balance { get; set; }
        public DateTime ContributedDate { get; set; }
        public string PaymentStatus { get; set; }
        public string Summary { get; set; }
        public int SpecialContributionId { get; set; }
    }
}
