using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class PaymentStatus
    {
        public int PaymentStatusId { get; private set; }
        public string PaymentStatusName { get; private set; }


        public PaymentStatus(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Payment status name cannot be empty");

            PaymentStatusName = name.Trim();
        }

        public static PaymentStatus Rehydrate(int id, string name)
        {
            return new PaymentStatus(name)
            {
                PaymentStatusId = id,
            };
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Payment status name cannot be empty");

            PaymentStatusName = newName.Trim();
        }

        public void SetId(int id)
        {
            PaymentStatusId = id;
        }
    }
}
