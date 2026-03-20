using System;

namespace APC.Applications.DTO
{
    public class EventImageDTO
    {
        public int Counter { get; set; }
        public int EventImageId { get; set; }
        public int EventId { get; set; }
        public string Summary { get; set; }
        public string ImagePath { get; set; }
        public string ImageCaption { get; set; }
    }
}
