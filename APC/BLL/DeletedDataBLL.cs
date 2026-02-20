using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class DeletedDataBLL
    {
        MemberDAO memberDAO = new MemberDAO();
        ProfessionDAO professionDAO = new ProfessionDAO();
        PositionDAO positionDAO = new PositionDAO();
        NationalityDAO nationalityDAO = new NationalityDAO();
        MaritalStatusDAO marStatusDAO = new MaritalStatusDAO();
        EmploymentStatusDAO empStatusDAO = new EmploymentStatusDAO();
        PermissionDAO permissionDAO = new PermissionDAO();
        CountryDAO countryDAO = new CountryDAO();
        MembershipStatusDAO memStatus = new MembershipStatusDAO();
        RelationshipToNextOfKinDAO kinsDAO = new RelationshipToNextOfKinDAO();
        FinedMemberDAO finedMemberDAO = new FinedMemberDAO();

        GeneralAttendanceDAO genAttendDAO = new GeneralAttendanceDAO();
        PersonalAttendanceDAO perAttendDAO = new PersonalAttendanceDAO();
        FinancialReportDAO finRepDAO = new FinancialReportDAO();
        ExpenditureDAO expenditureDAO = new ExpenditureDAO();
        CommentDAO commenntDAO = new CommentDAO();
        DocumentDAO documentDAO = new DocumentDAO();
        EventsDAO eventDAO = new EventsDAO();
        EventImageDAO eventImageDAO = new EventImageDAO();
        ConstitutionDAO constitutionDAO = new ConstitutionDAO();

        public DeletedDataDTO Select(bool isDeleted)
        {
            DeletedDataDTO dto = new DeletedDataDTO();
            dto.Members = memberDAO.Select(isDeleted);
            dto.Professions = professionDAO.Select(isDeleted);
            dto.Countries = countryDAO.Select(isDeleted);
            dto.Nationalities = nationalityDAO.Select(isDeleted);
            dto.EmploymentStatuses = empStatusDAO.Select(isDeleted);
            dto.Positions = positionDAO.Select(isDeleted);
            dto.MaritalStatuses = marStatusDAO.Select(isDeleted);
            dto.Comments = commenntDAO.Select(isDeleted);
            dto.Documents = documentDAO.Select(isDeleted);
            dto.EventImages = eventImageDAO.Select(isDeleted);
            dto.Events = eventDAO.Select(isDeleted);
            dto.Expenditures = expenditureDAO.Select(isDeleted);
            dto.FinancialReports = finRepDAO.Select(isDeleted);
            dto.GeneralAttendance = genAttendDAO.Select(isDeleted);
            dto.Constitutions = constitutionDAO.Select(isDeleted);
            dto.FinedMembers = finedMemberDAO.Select(isDeleted);
            return dto;
        }
    }
}
