using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class FinedMemberDTO
    {
        public List<FinedMemberDetailDTO> FineMembers { get; set; }
        public List<GenderDetailDTO> Genders { get; set; }
        public List<MemberDetailDTO> Members { get; set; }
        public List<ConstitutionDetailDTO> Constitutions { get; set; }
        public List<MonthDetailDTO> Months { get; set; }
    }
}
