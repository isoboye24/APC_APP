using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class FinancialReport
    {
        public int FinancialReportId { get; private set; }
        public decimal TotalAmountRaised { get; private set; }
        public decimal TotalAmountSpent { get; private set; }
        public int Year { get; private set; }
        public string Summary { get; private set; }

        public FinancialReport(decimal totalAmountRaised, decimal totalAmountSpent, int year, string summary)
        {
            SetTotalAmountRaised(totalAmountRaised);
            SetTotalAmountSpent(totalAmountSpent);
            SetYear(year);
            SetSummary(summary);
        }

        public static FinancialReport Rehydrate(
            int id,
           decimal totalAmountRaised, 
           decimal totalAmountSpent, 
           int year, 
           string summary
            )
        {
            var financialReport = new FinancialReport(totalAmountRaised, totalAmountSpent, year, summary);
            financialReport.FinancialReportId = id;
            return financialReport;
        }

        private void SetTotalAmountRaised(decimal amountRaised)
        {
            if (amountRaised < 0)
                throw new ArgumentException("Invalid Amount");

            TotalAmountRaised = amountRaised;
        }

        public void UpdateTotalAmountRaised(decimal newAmountRaised)
        {
            SetTotalAmountRaised(newAmountRaised);
        }

        private void SetTotalAmountSpent(decimal amountRaised)
        {
            if (amountRaised < 0)
                throw new ArgumentException("Invalid Amount");

            TotalAmountRaised = amountRaised;
        }

        public void UpdateTotalAmountSpent(decimal newAmountSpent)
        {
            SetTotalAmountSpent(newAmountSpent);
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

        private void SetYear(int year)
        {
            if (year < 2000 || year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            Year = year;
        }

        public void UpdateDate(int newYear)
        {
            SetYear(newYear);
        }
    }
}
