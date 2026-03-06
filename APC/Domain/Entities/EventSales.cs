using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class EventSales
    {
        public int EventSalesId { get; private set; }
        public int EventId { get; private set; }
        public decimal AmountSold { get; private set; }
        public string Summary { get; private set; }
        public int Day { get; private set; }
        public int MonthId { get; private set; }
        public int Year { get; private set; }
        public DateTime SalesDate { get; private set; }

        public EventSales(int eventId, decimal amountSold, string summary, int day, int monthId, int year, DateTime date)
        {
            SetEvent(eventId);
            SetSummary(summary);
            SetDate(date);
            SetAmountSold(amountSold);
        }

        public static EventSales Rehydrate(
            int id,
            int eventId,
            decimal amountSold,
            string summary,
            int day,
            int monthId,
            int year,
            DateTime date
            )
        {
            var eventSales = new EventSales(eventId, amountSold, summary, day, monthId, year, date);
            eventSales.EventSalesId = id;
            return eventSales;
        }

        private void SetSummary(string summary)
        {
            Summary = string.IsNullOrWhiteSpace(summary) ? null : summary.Trim();
        }

        public void UpdateSummary(string newSummary)
        {
            SetSummary(newSummary);
        }

        private void SetEvent(int eventId)
        {
            if (eventId <= 0)
                throw new ArgumentException("Invalid event");

            EventId = eventId;
        }

        private void SetDate(DateTime date)
        {
            if (date.Day < 1 || date.Day > 31)
                throw new ArgumentException("Invalid day");

            if (date.Month < 1 || date.Month > 12)
                throw new ArgumentException("Invalid month");

            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            SalesDate = date;
            Day = date.Day;
            MonthId = date.Month;
            Year = date.Year;
        }

        public void UpdateDate(DateTime newDate)
        {
            SetDate(newDate);
        }

        private void SetAmountSold(decimal amountSold)
        {
            if (amountSold < 0)
                throw new ArgumentException("Invalid Amount");

            AmountSold = amountSold;
        }

        public void UpdateAmountSold(decimal newAmountSold)
        {
            SetAmountSold(newAmountSold);
        }
    }
}
