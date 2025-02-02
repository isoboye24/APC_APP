using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class ChildDTO
    {
        public List<ChildDetailDTO> Children { get; set; }
        public List<NationalityDetailDTO> Nationalities { get; set; }
        public List<GenderDetailDTO> Genders { get; set; }
        public List<FathersDetailDTO> Fathers { get; set; }
        public List<MothersDetailDTO> Mothers { get; set; }
    }
}
