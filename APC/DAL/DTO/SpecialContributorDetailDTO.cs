using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class SpecialContributorDetailDTO
    {
        public int ContributorID { get; set; }
        public int MemberID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImagePath { get; set; }
        public string AmountContributed { get; set; }
        public string AmountContributedStatus { get; set; }
        public string AmountExpected { get; set; }
        public DateTime ContributedDate { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}
