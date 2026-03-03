using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public class MembershipStatus
    {
        public int MembershipStatusId { get; private set; }
        public string MembershipStatusName { get; private set; }

        public MembershipStatus(string name)
        {
            MembershipStatusName = name;
        }

        public static MembershipStatus Rehydrate(int id, string name)
        {
            return new MembershipStatus(name)
            {
                MembershipStatusId = id
            };
        }

        public void SetId(int id)
        {
            MembershipStatusId = id;
        }
    }
}
