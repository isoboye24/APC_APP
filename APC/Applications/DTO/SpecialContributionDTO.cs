using System;

namespace APC.Applications.DTO
{
    public class SpecialContributionDTO
    {
        public int SpecialContributionId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public decimal AmountToContribute { get; set; }
        public int SupervisorId { get; set; }
        public string Supervisor { get; set; }
        public string ImagePath { get; set; }
        public DateTime ContributionStartDate { get; set; }
        public string FormattedContributionStartDate { get; set; }
        public DateTime ContributionEndDate { get; set; }
        public string FormattedContributionEndDate { get; set; }
        public decimal AmountExpected { get; set; }
        public string Status { get; set; }
        public decimal TotalContributedAmount { get; set; }
    }
}
