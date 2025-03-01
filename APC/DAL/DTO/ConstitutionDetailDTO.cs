using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class ConstitutionDetailDTO
    {
        public int ConstitutionID { get; set; }
        public string ConstitutionText { get; set; }
        public string ShortDescription { get; set; }
        public string Section { get; set; }
        public decimal Fine { get; set; }
        public string FineWithCurrency { get; set; }
    }
}
