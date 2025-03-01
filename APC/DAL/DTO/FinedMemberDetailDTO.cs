using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class FinedMemberDetailDTO
    {
        public int FinedMemberID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ConstitutionSection { get; set; }
        public string ConstitutionShortDescription { get; set; }
        public decimal ExpectedAmount { get; set; }
        public string ExpectedAmountWithCurrency { get; set; }
        public decimal AmountPaid { get; set; }
        public string AmountPaidWithCurrency { get; set; }
        public decimal Balance { get; set; }
        public string BalanceWithCurrency { get; set; }
        public string FineStatus { get; set; }
        public string Gender { get; set; }
        public string Summary { get; set; }
        public int ConstitutionID { get; set; }
        public string Constitution { get; set; }
        public int MemberID { get; set; }
        public int PositionID { get; set; }
        public string Position { get; set; }
        public int GenderID { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public string ImagePath { get; set; }
    }
}
