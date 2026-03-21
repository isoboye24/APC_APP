using APC.Applications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface IMemberCommittmentService
    {
        List<MemberCommittmentDTO> GetMembersCommittment(int year);
    }
}
