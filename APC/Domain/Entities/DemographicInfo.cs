using System;

namespace APC.Domain.Entities
{
    public class DemographicInfo
    {
        public int CountryId { get; private set; }
        public int NationalityId { get; private set; }
        public int ProfessionId { get; private set; }
        public int EmploymentStatusId { get; private set; }
        public int MaritalStatusId { get; private set; }
        public string LGA { get; private set; }

        public DemographicInfo(int countryId, int nationalityId, int professionId, int employmentStatusId, int maritalStatusId, string lga)
        {
            SetCountry(countryId);
            SetNationality(nationalityId);
            SetProfession(professionId);
            SetEmploymentStatus(employmentStatusId);
            SetMaritalStatus(maritalStatusId);
            SetLGA(lga);
        }


        private void SetCountry(int countryId)
        {
            if (countryId < 0)
                throw new ArgumentException("Invalid country !!!");

            CountryId = countryId;
        }

        public void UpdateCountry(int newCountryId)
        {
            SetCountry(newCountryId);
        }

        private void SetNationality(int nationalityId)
        {
            if (nationalityId < 0)
                throw new ArgumentException("Invalid nationality !!!");

            NationalityId = nationalityId;
        }

        public void UpdateNationality(int newNationalityId)
        {
            SetNationality(newNationalityId);
        }
        
        private void SetProfession(int professionId)
        {
            if (professionId < 0)
                throw new ArgumentException("Invalid profession !!!");

            ProfessionId = professionId;
        }

        public void UpdateProfession(int newProfessionId)
        {
            SetProfession(newProfessionId);
        }
        
        private void SetEmploymentStatus(int employmentStatusId)
        {
            if (employmentStatusId < 0)
                throw new ArgumentException("Invalid employment status !!!");

            EmploymentStatusId = employmentStatusId;
        }

        public void UpdateEmplomentStatus(int newEmploymentStatusId)
        {
            SetEmploymentStatus(newEmploymentStatusId);
        }

        private void SetMaritalStatus(int maritalStatusId)
        {
            if (maritalStatusId < 0)
                throw new ArgumentException("Invalid marital status !!!");

            MaritalStatusId = maritalStatusId;
        }

        public void UpdateMaritalStatus(int newMaritalStatusId)
        {
            SetMaritalStatus(newMaritalStatusId);
        }

        private void SetLGA(string lga)
        {
            if (string.IsNullOrWhiteSpace(lga))
                throw new ArgumentException("LGA is required");

            LGA = lga.Trim();
        }

        public void UpdateLGA(string newLga)
        {
            SetLGA(newLga);
        }
    }
}
