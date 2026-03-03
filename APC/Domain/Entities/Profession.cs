using APC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class Profession
    {
        public int ProfessionId { get; private set; }
        public string ProfessionName { get; private set; }


        public Profession(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Profession name cannot be empty");

            ProfessionName = name.Trim();
        }

        public static Profession Rehydrate(int id, string name)
        {
            return new Profession(name)
            {
                ProfessionId = id,
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Profession name cannot be empty");

            ProfessionName = newName.Trim();
        }

        public void SetId(int id)
        {
            ProfessionId = id;
        }
    }
}
