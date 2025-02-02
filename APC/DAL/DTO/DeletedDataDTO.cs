using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class DeletedDataDTO
    {
        public List<MemberDetailDTO> Members { get; set; }
        public List<EmploymentStatusDetailDTO> EmploymentStatuses { get; set; }
        public List<ProfessionDetailDTO> Professions { get; set; }
        public List<PositionDetailDTO> Positions { get; set; }
        public List<MaritalStatusDetailDTO> MaritalStatuses { get; set; }
        public List<CountryDetailDTO> Countries { get; set; }
        public List<NationalityDetailDTO> Nationalities { get; set; }
        public List<ChildDetailDTO> Children { get; set; }
        public List<CommentDetailDTO> Comments { get; set; }
        public List<DocumentDetailDTO> Documents { get; set; }
        public List<EventImageDetailDTO> EventImages { get; set; }
        public List<EventsDetailDTO> Events { get; set; }
        public List<ExpenditureDetailDTO> Expenditures { get; set; }
        public List<FinancialReportDetailDTO> FinancialReports { get; set; }
        public List<GeneralAttendanceDetailDTO> GeneralAttendance { get; set; }
        public List<ConstitutionDetailDTO> Constitutions { get; set; }
        public List<FinedMemberDetailDTO> FinedMembers { get; set; }
    }
}
