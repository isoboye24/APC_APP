using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class EventImages
    {
        public int EventImagesId { get; private set; }
        public int EventId { get; private set; }
        public string Summary { get; private set; }
        public string ImagePath { get; private set; }
        public string ImageCaption { get; private set; }

        public EventImages(int eventId, string summary, string imagePath, string imageCaption)
        {
            SetEvent(eventId);
            SetSummary(summary);
            SetImagePath(imagePath);
            SetImageCaption(imageCaption);
        }

        public static EventImages Rehydrate(
            int id,
            int eventId,
            string summary, 
            string imagePath, 
            string imageCaption
            )
        {
            var eventImage = new EventImages(eventId, summary, imagePath, imageCaption);
            eventImage.EventImagesId = id;
            return eventImage;
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

        private void SetImageCaption(string caption)
        {
            ImageCaption = string.IsNullOrWhiteSpace(caption) ? null : caption.Trim();
        }

        public void UpdateImageCaption(string newCaption)
        {
            SetImageCaption(newCaption);
        }

        private void SetEvent(int eventId)
        {
            if (eventId <= 0)
                throw new ArgumentException("Invalid event");

            EventId = eventId;
        }
    }
}
