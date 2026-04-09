using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Entities
{
    public interface IMembershipStatusService
    {
        List<MembershipStatus> GetAll();

        MembershipStatus GetById(int id);
    }
}
