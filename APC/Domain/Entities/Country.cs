using APC.Domain.DTOs;
using APC.Domain.Entities.DTOs;
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

        public Country(CountryCreateState state)
        {
            if (string.IsNullOrWhiteSpace(state.CountryName))
                throw new ArgumentException("Country name cannot be empty");

            CountryName = state.CountryName.Trim();
        }

        public static Country Rehydrate(int id, CountryCreateState state)
        {
            return new Country(state)
            {
                CountryId = id,
            };
        }

        public void UpdateName(CountryUpdateState updateState)
        {
            if (string.IsNullOrWhiteSpace(updateState.CountryName))
                throw new ArgumentException("Country name cannot be empty");

            CountryName = updateState.CountryName.Trim();
        }

        public void SetId(int id)
        {
            CountryId = id;
        }
    }
}
