using System;

namespace APC.Domain.Entities
{
    public class Country
    {
        public int CountryId { get; private set; }
        public string CountryName { get; private set; }

        public Country(string name)
        {
            SetCountryName(name);
        }

        public static Country Rehydrate(int id, string name)
        {
            var country = new Country( name);
            country.CountryId = id;
            return country;
        }

        public void UpdateName(string newName)
        {
            SetCountryName(newName);
        }

        private void SetCountryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Country name cannot be empty");

            CountryName = name.Trim();
        }

        public void SetId(int id)
        {
            CountryId = id;
        }
    }
}
