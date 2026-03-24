using System;

namespace APC.Applications.DTO
{
    public class SpecialContributorDTO
    {
        public int SpecialContributorId { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public decimal AmountContributed { get; set; }
        public decimal AmountExpected { get; set; }
        public string Balance { get; set; }
        public string FormattedContributedDate { get; set; }
        public DateTime ContributedDate { get; set; }
        public string PaymentStatus { get; set; }
        public string Summary { get; set; }
        public int SpecialContributionId { get; set; }
    }
}
