using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class CommentDetailDTO
    {
        public int CommentID { get; set; }
        public string CommentName { get; set; }
        public int MemberID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public int Day { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public bool isMemberDeleted { get; set; }
    }
}
