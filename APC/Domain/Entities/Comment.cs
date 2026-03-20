using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; private set; }
        public string Content { get; private set; }
        public int MemberId { get; private set; }
        public DateTime Date { get; private set; }

        public Comment(string content, int memberId, DateTime date)
        {
            SetContent(content);
            SetMember(memberId);
            SetDate(date);
        }

        public static Comment Rehydrate(
            int id,
            string content,
            int memberId,
            DateTime date)
        {
            var comment = new Comment(content, memberId, date);
            comment.CommentId = id;
            return comment;
        }

        private void SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty");

            Content = content.Trim();
        }

        public void UpdateContent(string newContent)
        {
            SetContent(newContent);
        }

        private void SetMember(int memberId)
        {
            if (memberId <= 0)
                throw new ArgumentException("Invalid member");

            MemberId = memberId;
        }

        public void ChangeMember(int newMemberId)
        {
            SetMember(newMemberId);
        }

        private void SetDate(DateTime date)
        {
            
            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            Date = date;
        }

        public void ChangeDate(DateTime newDate)
        {
            SetDate(newDate);
        }
    }
}
