using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class PaymentStatusDetailDTO
    {
        public int PaymentStatusID { get; set; }
        public int PaymentStatusName { get; set; }
        public bool isDeleted { get; set; }
        public DateTime deletedDate { get; set; }
    }
}
