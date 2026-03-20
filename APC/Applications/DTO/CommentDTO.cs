using System;

namespace APC.Applications.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
        public string FormattedDate { get; set; }
    }
}
