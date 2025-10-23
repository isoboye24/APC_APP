using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class MembersCommittmentDetailDTO
    {
        public int MemberID { get; set; }
        public decimal ShowRank { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ImagePath { get; set; }
        public decimal ExpectedAmount { get; set; }
        public decimal Contributed { get; set; }
        public string Balance { get; set; }
        public decimal Fines { get; set; }
        public decimal PaidFines { get; set; }
        public int NumberOfPresence { get; set; }
        public int NumberOfAbsence { get; set; }
        public decimal Rank { get; set; }
    }
}
