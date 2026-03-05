using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class EventExpenditure
    {
        public int EventExpenditureId { get; private set; }
        public int EventId { get; private set; }
        public decimal SpentAmount { get; private set; }
        public DateTime ExpenditureDate { get; private set; }
        public string Summary { get; private set; }
        public int Day { get; private set; }
        public int MonthId { get; private set; }
        public int Year { get; private set; }

        public EventExpenditure(int eventId, decimal spentAmount, DateTime expenditureDate, string summary, int day, int monthId, int year)
        {
            SetEvent(eventId);
            SetSpentAmount(spentAmount);
            SetSummary(summary);
            SetDate(expenditureDate);
        }

        public static EventExpenditure Rehydrate(
            int id, 
            int eventId, 
            decimal spentAmount, 
            DateTime expenditureDate, 
            string summary, 
            int day, 
            int monthId, 
            int year
            )
        {
            var expenditure = new EventExpenditure(eventId, spentAmount, expenditureDate, summary, day, monthId, year);
            expenditure.EventExpenditureId = id;
            return expenditure;
        }

        private void SetSpentAmount(decimal spentAmount)
        {
            if (spentAmount < 0)
                throw new ArgumentException("Invalid Amount");

            SpentAmount = spentAmount;
        }

        public void UpdateSpentAmount(decimal newSpentAmount)
        {
            SetSpentAmount(newSpentAmount);
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
            if (date.Day < 1 || date.Day > 31)
                throw new ArgumentException("Invalid day");

            if (date.Month < 1 || date.Month > 12)
                throw new ArgumentException("Invalid month");

            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            ExpenditureDate = date;
            Day = date.Day;
            MonthId = date.Month;
            Year = date.Year;
        }

        public void UpdateDate(DateTime newDate)
        {
            SetDate(newDate);
        }

        private void SetEvent(int eventId)
        {
            if (eventId <= 0)
                throw new ArgumentException("Invalid event");

            EventId = eventId;
        }
    }
}
