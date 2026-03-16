using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class DeadMemberShortDetailDTO
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; }
        public string DeadDate { get; set; }
        public string Age { get; set; }
    }
}
