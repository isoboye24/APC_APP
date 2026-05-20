using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class EventDTO
    {
        public int EventsId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string CoverImagePath { get; set; }
        public DateTime EventsDate { get; set; }
        public String FormattedEventsDate { get; set; }
    }
}
