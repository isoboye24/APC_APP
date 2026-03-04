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
        public int Day { get; private set; }
        public int MonthId { get; private set; }
        public int Year { get; private set; }

        public Comment(string content, int memberId, int day, int monthId, int year)
        {
            SetContent(content);
            SetMember(memberId);
            SetDate(day, monthId, year);
        }

        public static Comment Rehydrate(
            int id,
            string content,
            int memberId,
            int day,
            int monthId,
            int year)
        {
            var comment = new Comment(content, memberId, day, monthId, year);
            comment.CommentId = id;
            return comment;
        }

        public void UpdateContent(string newContent)
        {
            SetContent(newContent);
        }

        public void ChangeMember(int newMemberId)
        {
            SetMember(newMemberId);
        }

        private void SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty");

            Content = content.Trim();
        }

        private void SetMember(int memberId)
        {
            if (memberId <= 0)
                throw new ArgumentException("Invalid member");

            MemberId = memberId;
        }

        private void SetDate(int day, int monthId, int year)
        {
            if (day < 1 || day > 31)
                throw new ArgumentException("Invalid day");

            if (monthId < 1 || monthId > 12)
                throw new ArgumentException("Invalid month");

            if (year < 2000 || year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            Day = day;
            MonthId = monthId;
            Year = year;
        }
    }
}
