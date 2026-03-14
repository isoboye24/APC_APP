using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class SpecialContribution
    {
        public int SpecialContributionId { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public decimal AmountToContribute { get; private set; }
        public int SupervisorId { get; private set; }
        public DateTime ContributionStartDate { get; private set; }
        public DateTime ContributionEndDate { get; private set; }
        public decimal AmountExpected { get; private set; }


        public SpecialContribution(string title, string summary, decimal amountToContribute, int supervisorId,
            DateTime contributionStartDate, DateTime contributionEndDate, decimal amountExpected)
        {
            SetTitle(title);
            SetSummary(summary);
            SetAmountToContribute(amountToContribute);
            SetSupervisor(supervisorId);
            SetContributionStartDate(contributionStartDate);
            SetContributionEndDate(contributionEndDate);
            SetAmountExpected(amountExpected);
        }

        public static SpecialContribution Rehydrate(
                int id,
                string title, 
                string summary, 
                decimal amountToContribute, 
                int supervisorId,
                DateTime contributionStartDate, 
                DateTime contributionEndDate, 
                decimal amountExpected
            )
        {
            var contribution = new SpecialContribution(title, summary, amountToContribute, supervisorId, contributionStartDate, 
                                                            contributionEndDate, amountExpected);
            contribution.SpecialContributionId = id;

            return contribution;
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            Title = title.Trim();
        }

        public void UpdateTitle(string newTitle)
        {
            SetTitle(newTitle);
        }

        private void SetSummary(string summary)
        {
            Summary = string.IsNullOrWhiteSpace(summary) ? null : summary.Trim();
        }

        public void UpdateSummary(string newSummary)
        {
            SetSummary(newSummary);
        }

        private void SetAmountToContribute(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Invalid amount");

            AmountToContribute = amount;
        }

        public void UpdateAmountToContribute(int newAmount)
        {
            SetAmountToContribute(newAmount);
        }
        
        private void SetSupervisor(int supervisorId)
        {
            if (supervisorId < 0)
                throw new ArgumentException("Invalid supervisor");

            SupervisorId = supervisorId;
        }

        public void UpdateSupervisor(int newSupervisorId)
        {
            SetSupervisor(newSupervisorId);
        }

        private void SetContributionStartDate(DateTime date)
        {
            if (date.Year < 2000)
                throw new ArgumentException("Invalid year");

            ContributionStartDate = date;
        }

        public void UpdateContributionStartDate(DateTime newDate)
        {
            SetContributionStartDate(newDate);
        }
        
        private void SetContributionEndDate(DateTime date)
        {
            if (date.Year < 2000)
                throw new ArgumentException("Invalid year");

            ContributionEndDate = date;
        }

        public void UpdateContributionEndDate(DateTime newDate)
        {
            SetContributionEndDate(newDate);
        }

        private void SetAmountExpected(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Invalid amount");

            AmountExpected = amount;
        }

        public void UpdateAmountExpected(int newAmount)
        {
            SetAmountExpected(newAmount);
        }
    }
}
