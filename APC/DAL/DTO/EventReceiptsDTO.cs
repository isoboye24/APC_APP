using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class EventReceiptsDTO
    {
        public List<EventReceiptsDetailDTO> EventReceipts {  get; set; }
        public List<MonthDetailDTO> Months {  get; set; }
    }
}
