using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DTO
{
    public class MemberDetailDTO
    {
        public int MemberID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string ImagePath { get; set; }
        public string EmailAddress { get; set; }
        public string HouseAddress { get; set; }
        public DateTime MembershipDate { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public int NationalityID { get; set; }
        public string NationalityName { get; set; }
        public int ProfessionID { get; set; }
        public string ProfessionName { get; set; }
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public int EmploymentStatusID { get; set; }
        public string EmploymentStatusName { get; set; }
        public int MaritalStatusID { get; set; }
        public string MaritalStatusName { get; set; }
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public bool isCountryDeleted { get; set; }
        public bool isNationalityDeleted { get; set; }
        public bool isProfessionDeleted { get; set; }
        public bool isPositionDeleted { get; set; }
        public bool isEmpStatusDeleted { get; set; }
        public bool isMarStatusDeleted { get; set; }
        public int MembershipStatusID { get; set; }
        public string MembershipStatus { get; set; }
        public DateTime DeadDate { get; set; }
        public double DeadAge { get; set; }
        public string LGA { get; set; }
        public string NameOfNextOfKin { get; set; }
        public int RelationshipToKinID { get; set; }
        public string RelationshipToKin { get; set; }
    }
}
