using System;

namespace APC.Applications.DTO
{
    
    public class MemberCommittmentDTO
    {
        public int MemberId { get; set; }
        public decimal Rank { get; set; }
        public decimal ShowRank { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public decimal ExpectedDues { get; set; }
        public string FormattedExpectedDues { get; set; }
        public decimal ContributedDues { get; set; }
        public string FormattedContributedDues { get; set; }
        public decimal BalanceDues { get; set; }
        public string FormattedBalanceDues { get; set; }
        public decimal TotalFines { get; set; }
        public string FormattedTotalFines { get; set; }
        public decimal PaidFines { get; set; }
        public string FormattedPaidFines { get; set; }
        public int NoOfPresent { get; set; }
        public int NoOfAbsent { get; set; }
        public string Status { get; set; }

    }
}
