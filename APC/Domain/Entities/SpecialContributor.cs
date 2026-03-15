using System;

namespace APC.Domain.Entities
{
    public class SpecialContributor
    {
        public int SpecialContributorId { get; private set; }
        public int MemberId { get; private set; }
        public decimal AmountContributed { get; private set; }
        public DateTime ContributedDate { get; private set; }
        public string Summary { get; private set; }
        public int SpecialContributionId { get; private set; }


        public SpecialContributor(int memberId, decimal amountContributed, DateTime contributedDate, string summary, int specialContributionId)
        {
            SetMember(memberId);
            SetAmountContributed(amountContributed);
            SetContributedDate(contributedDate);
            SetSummary(summary);
            SetSpecialContribution(specialContributionId);
        }

        public static SpecialContributor Rehydrate(
                int id,
                int memberId, 
                decimal amountContributed, 
                DateTime contributedDate, 
                string summary, 
                int specialContributionId
            )
        {
            var contributor = new SpecialContributor(memberId, amountContributed, contributedDate, summary, specialContributionId);
            contributor.SpecialContributorId = id;

            return contributor;
        }

        private void SetMember(int memberId)
        {
            if (memberId < 0)
                throw new ArgumentException("Invalid Contributor");

            MemberId = memberId;
        }

        public void UpdateMember(int newMemberId)
        {
            SetMember(newMemberId);
        }

        private void SetAmountContributed(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Invalid amount");

            AmountContributed = amount;
        }

        public void UpdateAmountContributed(decimal newAmount)
        {
            SetAmountContributed(newAmount);
        }

        private void SetContributedDate(DateTime date)
        {
            if (date.Year < 2000)
                throw new ArgumentException("Invalid year");

            ContributedDate = date;
        }

        public void UpdateContributedDate(DateTime newDate)
        {
            SetContributedDate(newDate);
        }

        private void SetSummary(string summary)
        {
            Summary = string.IsNullOrWhiteSpace(summary) ? null : summary.Trim();
        }

        public void UpdateSummary(string newSummary)
        {
            SetSummary(newSummary);
        }

        private void SetSpecialContribution(int specialContributionId)
        {
            if (specialContributionId < 0)
                throw new ArgumentException("Invalid Special Contribution");

            SpecialContributionId = specialContributionId;
        }

        public void UpdateAmountExpected(int newSpecialContributionId)
        {
            SetSpecialContribution(newSpecialContributionId);
        }
    }
}
