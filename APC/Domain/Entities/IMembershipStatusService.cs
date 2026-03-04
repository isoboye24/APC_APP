using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Entities
{
    public interface IMembershipStatusService
    {
        List<MembershipStatus> GetAll();
    }
}
