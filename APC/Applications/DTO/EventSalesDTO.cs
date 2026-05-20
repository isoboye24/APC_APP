using System;

namespace APC.Applications.DTO
{
    public class EventSalesDTO
    {
        public int EventSalesId { get; set; }
        public int EventId { get; set; }
        public decimal AmountSold { get; set; }
        public string Summary { get; set; }
        public DateTime SalesDate { get; set; }
    }
}
