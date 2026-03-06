using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class TheEvents
    {
        public int EventsId { get; private set; }
        public string Title { get; private set; }
        public string Summary { get; private set; }
        public string CoverImagePath { get; private set; }
        public DateTime EventsDate { get; private set; }

        public TheEvents(string title, string summary, string coverImagePath, DateTime eventsDate)
        {
            SetTitle(title);
            SetSummary(summary);
            SetImagePath(coverImagePath);
            SetDate(eventsDate);
        }

        public static TheEvents Rehydrate(
            int id,
            string title,
            string summary,
            string coverImagePath,
            DateTime eventsDate
            )
        {
            var events = new TheEvents(title, summary, coverImagePath, eventsDate);
            events.EventsId = id;
            return events;
        }

        private void SetSummary(string summary)
        {
            Summary = string.IsNullOrWhiteSpace(summary) ? null : summary.Trim();
        }

        public void UpdateSummary(string newSummary)
        {
            SetSummary(newSummary);
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

        private void SetDate(DateTime date)
        {
            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            EventsDate = date;
        }

        public void UpdateDate(DateTime newDate)
        {
            SetDate(newDate);
        }

        private void SetImagePath(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Image cannot be empty");

            CoverImagePath = imagePath.Trim();
        }

        public void UpdateImagePath(string newImagePath)
        {
            SetImagePath(newImagePath);
        }
    }
}
