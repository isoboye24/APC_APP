using APC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class EmploymentStatus
    {
        public int EmploymentStatusId { get; private set; }
        public string EmploymentStatusName { get; private set; }


        public EmploymentStatus(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Employment status name cannot be empty");

            EmploymentStatusName = name.Trim();
        }

        public static EmploymentStatus Rehydrate(int id, string name)
        {
            return new EmploymentStatus(name)
            {
                EmploymentStatusId = id,
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Employment status name cannot be empty");

            EmploymentStatusName = newName.Trim();
        }

        public void SetId(int id)
        {
            EmploymentStatusId = id;
        }
    }
}
