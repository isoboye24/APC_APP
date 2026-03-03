using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Nationality
    {
        public int NationalityId { get; private set; }
        public string NationalityName { get; private set; }


        public Nationality(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nationality name cannot be empty");

            NationalityName = name.Trim();
        }

        public static Nationality Rehydrate(int id, string name)
        {
            return new Nationality(name)
            {
                NationalityId = id,
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Nationality name cannot be empty");

            NationalityName = newName.Trim();
        }

        public void SetId(int id)
        {
            NationalityId = id;
        }
    }
}
