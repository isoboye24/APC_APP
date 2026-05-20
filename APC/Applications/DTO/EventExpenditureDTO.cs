using System;

namespace APC.Applications.DTO
{
    public class EventExpenditureDTO
    {
        public int EventExpenditureId { get; set; }
        public int EventId { get; set; }
        public decimal SpentAmount { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public string Summary { get; set; }

    }
}
