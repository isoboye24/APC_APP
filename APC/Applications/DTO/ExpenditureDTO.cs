using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class ExpenditureDTO
    {
        public int ExpenditureId { get; set; }
        public decimal AmountSpent { get; set; }
        public string Summary { get; set; }
        public DateTime ExpenditureDate { get; set; }
        public string FormattedExpenditureDate { get; set; }
    }
}
