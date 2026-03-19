using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public int Day { get; set; }
        public int MonthId { get; set; }
        public int Year { get; set; }
    }
}
