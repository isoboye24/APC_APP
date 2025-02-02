using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class MemberDTO
    {
        public List<MemberDetailDTO> Members { get; set; }
        public List<GenderDetailDTO> Genders { get; set; }
        public List<EmploymentStatusDetailDTO> EmploymentStatuses { get; set; }
        public List<ProfessionDetailDTO> Professions { get; set; }
        public List<PositionDetailDTO> Positions { get; set; }
        public List<MaritalStatusDetailDTO> MaritalStatuses { get; set; }
        public List<CountryDetailDTO> Countries { get; set; }
        public List<NationalityDetailDTO> Nationalities { get; set; }
        public List<DualNationalityDetailDTO> DualNationalities { get; set; }
        public List<FathersDetailDTO> Fathers { get; set; }
        public List<MothersDetailDTO> Mothers { get; set; }
        public List<PermissionDetailDTO> Permissions { get; set; }
        public List<MembershipStatusDetailDTO> MembershipStatuses { get; set; }
        public List<AbsenteesDetailDTO> Absentees { get; set; }
        public List<RelationshipToNextOfKinDetailDTO> RelationshipsToNextOfKin { get; set; }
    }
}
