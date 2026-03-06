using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Expenditure
    {
        public int ExpenditureId { get; private set; }
        public decimal AmountSpent { get; private set; }
        public string Summary { get; private set; }
        public DateTime ExpenditureDate { get; private set; }

        public Expenditure(decimal amountSpent, string summary, DateTime expenditureDate)
        {
            SetAmountSpent(amountSpent);
            SetSummary(summary);
            SetDate(expenditureDate);
        }

        public static Expenditure Rehydrate(
            int id,
            decimal amountSpent, 
            string summary, 
            DateTime expenditureDate
            )
        {
            var expenditure = new Expenditure(amountSpent, summary, expenditureDate);
            expenditure.ExpenditureId = id;
            return expenditure;
        }

        private void SetAmountSpent(decimal amountSpent)
        {
            if (amountSpent < 0)
                throw new ArgumentException("Invalid Amount");

            AmountSpent = amountSpent;
        }

        public void UpdateSpentAmount(decimal newSpentAmount)
        {
            SetAmountSpent(newSpentAmount);
        }

        private void SetSummary(string summary)
        {
            if (string.IsNullOrWhiteSpace(summary))
                throw new ArgumentException("Summary cannot be empty");

            Summary = summary.Trim();
        }

        public void UpdateSummary(string newSummary)
        {
            SetSummary(newSummary);
        }

        private void SetDate(DateTime date)
        {
            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            ExpenditureDate = date;
        }

        public void UpdateDate(DateTime newDate)
        {
            SetDate(newDate);
        }
    }
}
