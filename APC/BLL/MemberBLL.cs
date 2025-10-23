using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.BLL
{
    public class MemberBLL : IBLL<MemberDTO, MemberDetailDTO>
    {
        MemberDAO memberDAO = new MemberDAO();
        ProfessionDAO professionDAO = new ProfessionDAO();
        PositionDAO positionDAO = new PositionDAO();
        NationalityDAO nationalityDAO = new NationalityDAO();
        GenderDAO genderDAO = new GenderDAO();
        MaritalStatusDAO marStatusDAO = new MaritalStatusDAO();
        EmploymentStatusDAO empStatusDAO = new EmploymentStatusDAO();
        PermissionDAO permissionDAO = new PermissionDAO();
        CountryDAO countryDAO = new CountryDAO();
        MembershipStatusDAO memStatus = new MembershipStatusDAO();
        RelationshipToNextOfKinDAO kinsDAO = new RelationshipToNextOfKinDAO();
        FinedMemberDAO finedMemberDAO = new FinedMemberDAO();

        // These classes are here for the sake of deletedData form
        GeneralAttendanceDAO genAttendDAO = new GeneralAttendanceDAO();
        ChildDAO childDAO = new ChildDAO();
        PersonalAttendanceDAO perAttendDAO = new PersonalAttendanceDAO();
        FinancialReportDAO finRepDAO = new FinancialReportDAO();
        ExpenditureDAO expenditureDAO = new ExpenditureDAO();
        CommentDAO commenntDAO = new CommentDAO();
        DocumentDAO documentDAO = new DocumentDAO();
        EventsDAO eventDAO = new EventsDAO();
        EventImageDAO eventImageDAO = new EventImageDAO();
        ConstitutionDAO constitutionDAO = new ConstitutionDAO();

        public bool Delete(MemberDetailDTO entity)
        {
            MEMBER member = new MEMBER();
            member.memberID = entity.MemberID;
            return memberDAO.Delete(member);
        }

        public bool DeletePermission(MemberDetailDTO entity)
        {
            MEMBER member = new MEMBER();
            member.memberID = entity.MemberID;
            return memberDAO.DeletePermission(member);
        }

        public bool GetBack(MemberDetailDTO entity)
        {
            return memberDAO.GetBack(entity.MemberID);
        }

        public bool Insert(MemberDetailDTO entity)
        {
            MEMBER member = new MEMBER();
            member.username = entity.Username;
            member.password = entity.Password;
            member.name = entity.Name;
            member.surname = entity.Surname;
            member.birthday = entity.Birthday;
            member.imagePath = entity.ImagePath;
            member.houseAddress = entity.HouseAddress;
            member.emailAddress = entity.EmailAddress;
            member.membershipDate = entity.MembershipDate;
            member.countryID = entity.CountryID;
            member.nationalityID = entity.NationalityID;
            member.professionID = entity.ProfessionID;
            member.positionID = entity.PositionID;
            member.genderID = entity.GenderID;
            member.employmentStatusID = entity.EmploymentStatusID;
            member.maritalStatusID = entity.MaritalStatusID;
            member.permissionID = entity.PermissionID;
            member.phoneNumber = entity.PhoneNumber;
            member.phoneNumber2 = entity.PhoneNumber2;
            member.phoneNumber3 = entity.PhoneNumber3;
            member.deadDate = entity.DeadDate;
            member.LGAOfCountryOrigin = entity.LGA;
            member.membershipStatusID = entity.MembershipStatusID;
            member.nextOfKin = entity.NameOfNextOfKin;
            member.relationshipToKinID = entity.RelationshipToKinID;
            return memberDAO.Insert(member);
        }

        public MemberDTO Select()
        {
            MemberDTO dto = new MemberDTO();
            dto.Members = memberDAO.Select();
            dto.Professions = professionDAO.Select();
            dto.Countries = countryDAO.Select();
            dto.Nationalities = nationalityDAO.Select();
            dto.EmploymentStatuses = empStatusDAO.Select();
            dto.Genders = genderDAO.Select();
            dto.Positions = positionDAO.Select();
            dto.MaritalStatuses = marStatusDAO.Select();
            dto.Permissions = permissionDAO.Select();
            dto.MembershipStatuses = memStatus.Select();
            dto.Absentees = memberDAO.Select3MonthsAbsentes();
            dto.RelationshipsToNextOfKin = kinsDAO.Select();
            return dto;
        }

        public MemberDTO Select(int memberID)
        {
            MemberDTO dto = new MemberDTO();
            dto.Members = memberDAO.Select(memberID);
            return dto;
        }
        public MemberDTO SelectFormerMembers()
        {
            MemberDTO dto = new MemberDTO();
            dto.Members = memberDAO.SelectFormerMembers();
            dto.Countries = countryDAO.Select();
            dto.Nationalities = nationalityDAO.Select();
            dto.Genders = genderDAO.Select();
            dto.Positions = positionDAO.Select();
            dto.Professions = professionDAO.Select();
            return dto;
        }

        public MemberDTO SelectDeadMembers()
        {
            MemberDTO dto = new MemberDTO();
            dto.Members = memberDAO.SelectDeadMembers();
            dto.Professions = professionDAO.Select();
            dto.Countries = countryDAO.Select();
            dto.Nationalities = nationalityDAO.Select();
            dto.Genders = genderDAO.Select();
            dto.Positions = positionDAO.Select();
            dto.MaritalStatuses = marStatusDAO.Select();
            return dto;
        }

        public List<MEMBER> CheckMember(string password, string username)
        {
            return memberDAO.CheckMember(password, username);
        }
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
            dto.Children = childDAO.Select(isDeleted);
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
        public int SelectAllMembersCount()
        {
            return memberDAO.SelectAllMembersCount();
        }
        public string GetLastMemberUsername()
        {
            return memberDAO.GetLastMemberUsername();
        }
        public int GetNoOfMembersPresentAttendance(int ID)
        {
            return memberDAO.GetNoOfMembersPresentAttendance(ID);
        }
        public int GetNoOfMembersAbsentAttendance(int ID)
        {
            return memberDAO.GetNoOfMembersAbsentAttendance(ID);
        }
        public decimal GetFinedAmountPaid(int ID)
        {
            return finedMemberDAO.GetFinedAmountPaid(ID);
        }
        public decimal GetFinedAmountExpected(int ID)
        {
            return finedMemberDAO.GetFinedAmountExpected(ID);
        }
        public decimal GetAmountContributed(int ID)
        {
            return memberDAO.GetAmountContributed(ID);
        }
        // To be edited to make it been specific to each member
        public decimal GetAmountExpected(int ID)
        {
            return memberDAO.GetAmountExpected(ID);
        }
        public int Select3MonthsAbsentesCount()
        {
            return memberDAO.Select3MonthsAbsentesCount();
        }
        public int SelectCountMale()
        {
            return memberDAO.SelectCountMale();
        }
        public int SelectCountDeadMale()
        {
            return memberDAO.SelectCountDeadMale();
        }
        public int SelectCountFormerMale()
        {
            return memberDAO.SelectCountFormerMale();
        }
        public int SelectCountFemale()
        {
            return memberDAO.SelectCountFemale();
        }
        public int SelectCountDeadFemale()
        {
            return memberDAO.SelectCountDeadFemale();
        }
        public int SelectCountFormerFemale()
        {
            return memberDAO.SelectCountFormerFemale();
        }
        public int SelectCountDivisor()
        {
            return memberDAO.SelectCountDivisor();
        }
        public int SelectCountDeadDivisor()
        {
            return memberDAO.SelectCountDeadDivisor();
        }
        public int SelectCountFormerDivisor()
        {
            return memberDAO.SelectCountFormerDivisor();
        }
        public int SelectCountUniqueNationality()
        {
            return memberDAO.SelectCountUniqueNationality();
        }

        public bool Update(MemberDetailDTO entity)
        {
            MEMBER member = new MEMBER();
            member.memberID = entity.MemberID;
            member.surname = entity.Surname;
            member.name = entity.Name;
            member.username = entity.Username;
            member.password = entity.Password;
            member.birthday = entity.Birthday;
            member.imagePath = entity.ImagePath;
            member.houseAddress = entity.HouseAddress;
            member.emailAddress = entity.EmailAddress;
            member.membershipDate = entity.MembershipDate;
            member.countryID = entity.CountryID;
            member.nationalityID = entity.NationalityID;
            member.professionID = entity.ProfessionID;
            member.positionID = entity.PositionID;
            member.genderID = entity.GenderID;
            member.employmentStatusID = entity.EmploymentStatusID;
            member.maritalStatusID = entity.MaritalStatusID;
            member.permissionID = entity.PermissionID;
            member.phoneNumber = entity.PhoneNumber;
            member.phoneNumber2 = entity.PhoneNumber2;
            member.phoneNumber3 = entity.PhoneNumber3;
            member.deadDate = entity.DeadDate;
            member.LGAOfCountryOrigin = entity.LGA;
            member.membershipStatusID = entity.MembershipStatusID;
            member.nextOfKin = entity.NameOfNextOfKin;
            member.relationshipToKinID = entity.RelationshipToKinID;
            return memberDAO.Update(member);
        }
    }
}
