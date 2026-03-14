using System;

namespace APC.Applications.DTO
{
    public class SpecialContributionFullDetails
    {
        public int SpecialContributionId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public decimal AmountToContribute { get; set; }
        public int SupervisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public DateTime ContributionStartDate { get; set; }
        public DateTime ContributionEndDate { get; set; }
        public decimal AmountExpected { get; set; }
    }
}
