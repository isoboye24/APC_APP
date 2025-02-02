using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class EventImageDetailDTO
    {
        public int EventImageID { get; set; }
        public int EventID { get; set; }
        public string EventTitle { get; set; }
        public int EventYear { get; set; }
        public string Summary { get; set; }
        public string ImagePath { get; set; }
        public int Counter { get; set; }
        public string ImageCaption { get; set; }
        public bool isEventDeleted { get; set; }
    }
}
