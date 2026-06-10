using System;

namespace APC.Applications.DTO
{
    public class EventReceiptDTO
    {
        public int Counter { get; set; }
        public int EventReceiptId { get; set; }
        public int EventId { get; set; }
        public string ImagePath { get; set; }
        public string Summary { get; set; }
        public string Caption { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string FormattedReceiptDate { get; set; }
        public decimal AmountSpent { get; set; }
        public string FormattedAmountSpent { get; set; }
    }
}
