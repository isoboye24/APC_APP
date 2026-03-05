using APC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.Domain.Entities
{    
    public class EventReceipt
    {
        public int EventReceiptId { get; private set; }
        public int EventId { get; private set; }
        public string ImagePath { get; private set; }
        public string Summary { get; private set; }
        public string Caption { get; private set; }
        public int Day { get; private set; }
        public int MonthId { get; private set; }
        public int Year { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public decimal AmountSpent { get; private set; }

        public EventReceipt(int eventId, string imagePath, string summary, string caption, int day, int monthId, int year, DateTime date,
            decimal amountSpent)
        {
            SetEvent(eventId);
            SetSummary(summary);
            SetImagePath(imagePath);
            SetCaption(caption);
            SetDate(date);
            SetAmountSpent(amountSpent);
        }

        public static EventReceipt Rehydrate(
            int id,
            int eventId,
            string imagePath,
            string summary,
            string imageCaption,
            int day, 
            int monthId, 
            int year, 
            DateTime date,
            decimal amountSpent
            )
        {
            var eventReceipt = new EventReceipt(eventId, summary, imagePath, imageCaption, day, monthId, year, date,
            amountSpent);
            eventReceipt.EventReceiptId = id;
            return eventReceipt;
        }

        private void SetSummary(string summary)
        {
            Summary = string.IsNullOrWhiteSpace(summary) ? null : summary.Trim();
        }

        public void UpdateSummary(string newSummary)
        {
            SetSummary(newSummary);
        }

        private void SetImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Image cannot be empty");

            ImagePath = imagePath.Trim();
        }

        public void UpdateImagePath(string newImagePath)
        {
            SetImagePath(newImagePath);
        }

        private void SetCaption(string caption)
        {
            Caption = string.IsNullOrWhiteSpace(caption) ? null : caption.Trim();
        }

        public void UpdateImageCaption(string newCaption)
        {
            SetCaption(newCaption);
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

            ReceiptDate = date;
            Day = date.Day;
            MonthId = date.Month;
            Year = date.Year;
        }

        public void UpdateDate(DateTime newDate)
        {
            SetDate(newDate);
        }

        private void SetAmountSpent(decimal amountSpent)
        {
            if (amountSpent < 0)
                throw new ArgumentException("Invalid Amount");

            AmountSpent = amountSpent;
        }

        public void UpdateSpentAmount(decimal newAmountSpent)
        {
            SetAmountSpent(newAmountSpent);
        }
    }
}
