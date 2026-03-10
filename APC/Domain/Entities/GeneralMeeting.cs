using System;

namespace APC.Domain.Entities
{
    public class GeneralMeeting
    {
        public int GeneralMeetingId { get; private set; }
        public int? TotalMembersPresent { get; private set; }
        public int? TotalMembersAbsent { get; private set; }
        public decimal? TotalDuesPaid { get; private set; }
        public decimal? TotalDuesExpected { get; private set; }
        public decimal? TotalDuesBalance { get; private set; }
        public string Summary { get; private set; }
        public DateTime GeneralMeetingDate { get; private set;
        }

        public GeneralMeeting(int? totalMembersPresent, int? totalMembersAbsent, decimal? totalDuesPaid, decimal? totalDuesExpected,
            decimal? totalDuesBalance, string summary, DateTime generalMeetingDate)
        {
            SetTotalMembersPresent(totalMembersPresent);
            SetTotalMembersAbsent(totalMembersAbsent);
            SetTotalDuesPaid(totalDuesPaid);
            SetTotalDuesExpected(totalDuesExpected);
            SetTotalDuesBalance(totalDuesBalance);
            SetSummary(summary);
            SetGeneralMeetingDate(generalMeetingDate);
        }

        public static GeneralMeeting Rehydrate(
                int id,
                int? totalMembersPresent, 
                int? totalMembersAbsent, 
                decimal? totalDuesPaid, 
                decimal? totalDuesExpected,
                decimal? totalDuesBalance, 
                string summary, 
                DateTime generalMeetingDate
            )
        {
            var generalMeeting = new GeneralMeeting(totalMembersPresent, totalMembersAbsent, totalDuesPaid, totalDuesExpected, totalDuesBalance, 
                summary, generalMeetingDate);
            generalMeeting.GeneralMeetingId = id;
            return generalMeeting;
        }

        private void SetTotalMembersPresent(int? membersPresent)
        {
            var value = membersPresent ?? 0;
            TotalMembersPresent = value < 0 ? 0 : value;
        }

        public void UpdateTotalMembersPresent(int newMembersPresent)
        {
            SetTotalMembersPresent(newMembersPresent);
        }

        private void SetTotalMembersAbsent(int? membersAbsent)
        {
            var value = membersAbsent ?? 0;
            TotalMembersAbsent = value < 0 ? 0 : value;
        }

        public void UpdateTotalMembersAbsent(int newMembersAbsent)
        {
            SetTotalMembersAbsent(newMembersAbsent);
        }
       
        private void SetTotalDuesPaid(decimal? duesPaid)
        {
            var value = duesPaid ?? 0;
            TotalDuesPaid = value < 0 ? 0 : value;
        }

        public void UpdateTotalDuesPaid(decimal newDuesPaid)
        {
            SetTotalDuesPaid(newDuesPaid);
        }

        private void SetTotalDuesExpected(decimal? duesExpected)
        {
            var value = duesExpected ?? 0;
            TotalDuesPaid = value < 0 ? 0 : value;
        }

        public void UpdateTotalDuesExpected(decimal newDuesExpected)
        {
            SetTotalDuesExpected(newDuesExpected);
        }
        
        private void SetTotalDuesBalance(decimal? duesBalance)
        {
            var value = duesBalance ?? 0;
            TotalDuesPaid = value < 0 ? 0 : value;
        }

        public void UpdateTotalDuesBalance(decimal newDuesBalance)
        {
            SetTotalDuesBalance(newDuesBalance);
        }

        private void SetSummary(string summary)
        {
            Summary = string.IsNullOrWhiteSpace(summary) ? null : summary.Trim();
        }

        public void UpdateSummary(string newSummary)
        {
            SetSummary(newSummary);
        }

        private void SetGeneralMeetingDate(DateTime date)
        {
            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            GeneralMeetingDate = date;
        }

        public void UpdateGeneralMeetingDate(DateTime newDate)
        {
            SetGeneralMeetingDate(newDate);
        }
    }
}
