using System;

namespace APC.Applications.DTO
{
    
    public class MemberCommittment
    {
        public int MemberId { get; set; }
        public decimal Rank { get; set; }
        public decimal ShowRank { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public decimal ExpectedDues { get; set; }
        public decimal ContributedDues { get; set; }
        public decimal BalanceDues { get; set; }
        public decimal TotalFines { get; set; }
        public decimal PaidFines { get; set; }
        public int NoOfPresent { get; set; }
        public int NoOfAbsent { get; set; }
        public string Status { get; set; }

    }
}
