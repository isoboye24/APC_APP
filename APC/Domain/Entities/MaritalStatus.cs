using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class MaritalStatus
    {
        public int MaritalStatusId { get; private set; }
        public string MaritalStatusName { get; private set; }


        public MaritalStatus(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Marital status name cannot be empty");

            MaritalStatusName = name.Trim();
        }

        public static MaritalStatus Rehydrate(int id, string name)
        {
            return new MaritalStatus(name)
            {
                MaritalStatusId = id,
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Marital status name cannot be empty");

            MaritalStatusName = newName.Trim();
        }

        public void SetId(int id)
        {
            MaritalStatusId = id;
        }
    }
}
