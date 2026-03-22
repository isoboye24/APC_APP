using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class FinedMemberDTO
    {
        public int FinedMemberId { get; set; }
        public decimal AmountPaid { get; set; }
        public string FormattedAmountPaid { get; set; }
        public string Summary { get; set; }
        public int ConstitutionId { get; set; }
        public string Section { get; set; }
        public string ShortDescription { get; set; }
        public string AmountExpected { get; set; }
        public string Balance { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string Status { get; set; }
        public DateTime FineDate { get; set; }
        public string FormattedFineDate { get; set; }
    }
}
