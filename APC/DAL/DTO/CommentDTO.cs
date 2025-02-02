using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class CommentDTO
    {
        public List<CommentDetailDTO> Comments { get; set; }
        public List<MemberDetailDTO> Members { get; set; }
        public List<GenderDetailDTO> Genders { get; set; }
        public List<MonthDetailDTO> Months { get; set; }
    }
}
