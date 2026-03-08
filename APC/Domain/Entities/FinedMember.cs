using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class FinedMember
    {
        public int FinedMemberId { get; private set; }
        public decimal AmountPaid { get; private set; }
        public string Summary { get; private set; }
        public int ConstitutionId { get; private set; }
        public int MemberId { get; private set; }
        public DateTime FineDate { get; private set; }

        public FinedMember(decimal amountPaid, string summary,  int constitutionId, int memberId, DateTime fineDate)
        {
            SetAmountPaid(amountPaid);
            SetSummary(summary);
            SetConstitution(constitutionId);
            SetMember(memberId);
            SetFineDate(fineDate);
        }

        public static FinedMember Rehydrate(
                int id,
               decimal amountPaid, 
               string summary, 
               int constitutionId, 
               int memberId, 
               DateTime fineDate
            )
        {
            var finedMember = new FinedMember(amountPaid, summary, constitutionId, memberId, fineDate);
            finedMember.FinedMemberId = id;
            return finedMember;
        }

        private void SetAmountPaid(decimal amountPaid)
        {
            AmountPaid = amountPaid < 0 ? 0 : amountPaid;
        }

        public void UpdateAmountPaid(decimal newAmountPaid)
        {
            SetAmountPaid(newAmountPaid);
        }

        private void SetSummary(string summary)
        {
            Summary = string.IsNullOrWhiteSpace(summary) ? null : summary.Trim();
        }

        public void UpdateSummary(string newSummary)
        {
            SetSummary(newSummary);
        }

        private void SetConstitution(int id)
        {
            if (id < 0)
                throw new ArgumentException("Invalid Constitution");

            ConstitutionId = id;
        }

        public void UpdateConstitution(int newId)
        {
            SetConstitution(newId);
        }

        private void SetMember(int id)
        {
            if (id < 0)
                throw new ArgumentException("Invalid Member");

            MemberId = id;
        }

        public void UpdateMember(int newId)
        {
            SetMember(newId);
        }

        private void SetFineDate(DateTime date)
        {
            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year");

            FineDate = date;
        }

        public void UpdateFineDate(DateTime newDate)
        {
            SetFineDate(newDate);
        }
    }
}
