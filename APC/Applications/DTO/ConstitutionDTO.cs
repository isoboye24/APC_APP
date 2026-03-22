using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class ConstitutionDTO
    {
        public int ConstitutionId { get; set; }
        public string ConstitutionText { get; set; }
        public decimal Fine { get; set; }
        public string Section { get; set; }
        public string ShortDescription { get; set; }
    }
}
