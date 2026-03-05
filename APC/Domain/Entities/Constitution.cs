using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Constitution
    {
        public int ConstitutionId { get; private set; }
        public string ConstitutionText { get; private set; }
        public decimal Fine { get; private set; }
        public string Section { get; private set; }
        public string ShortDescription { get; private set; }

        public Constitution(string constitutionText, decimal fine, string section, string shortDescription)
        {
            SetConstitutionText(constitutionText);
            SetFine(fine);
            SetSection(section);
            SetShortDescription(shortDescription);
        }

        public static Constitution Rehydrate(
            int id,
            string constitutionText,
            decimal fine,
            string section,
            string shortDescription
            )
        {
            var constitution = new Constitution(constitutionText, fine, section, shortDescription);
            constitution.ConstitutionId = id;
            return constitution;
        }

        private void SetConstitutionText(string constitutionText)
        {
            if (string.IsNullOrWhiteSpace(constitutionText))
                throw new ArgumentException("Constitution cannot be empty");

            ConstitutionText = constitutionText.Trim();
        }

        public void UpdateConstitutionText(string newConstitutionText)
        {
            SetConstitutionText(newConstitutionText);
        }

        private void SetFine(decimal fine)
        {
            if (fine < 0)
                throw new ArgumentException("Invalid fine");

            Fine = fine;
        }

        public void UpdateFine(decimal newFine)
        {
            SetFine(newFine);
        }

        private void SetSection(string section)
        {
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("Section cannot be empty");

            Section = section.Trim();
        }

        public void UpdateSection(string newSection)
        {
            SetSection(newSection);
        }

        private void SetShortDescription(string shortDescription)
        {
            if (string.IsNullOrWhiteSpace(shortDescription))
                throw new ArgumentException("Short description cannot be empty");

            ShortDescription = shortDescription.Trim();
        }

        public void UpdateShortDescription(string newDescription)
        {
            SetShortDescription(newDescription);
        }
    }
}
