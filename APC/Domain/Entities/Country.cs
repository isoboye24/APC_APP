using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Country
    {
        public int CountryId { get; private set; }
        public string CountryName { get; private set; }

        public Country(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Country name cannot be empty");

            CountryName = name.Trim();
        }

        public static Country Rehydrate(int id, string name)
        {
            return new Country(name)
            {
                CountryId = id,
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Country name cannot be empty");

            CountryName = newName.Trim();
        }

        public void SetId(int id)
        {
            CountryId = id;
        }
    }
}
