using APC.Applications.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IMemberCommittmentRepository
    {
        List<MemberCommittmentDTO> GetMembersCommittment(int year);
    }
}
