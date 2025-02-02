using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class PermissionDTO
    {
        public List<PermissionDetailDTO> Permissions { get; set; }
        public List<MemberDetailDTO> Members { get; set; }
    }
}
