using System;

namespace APC.Applications.DTO
{
    public class MembersBasicDetailDTO
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; }
        public string Permission { get; set; }
    }
}
