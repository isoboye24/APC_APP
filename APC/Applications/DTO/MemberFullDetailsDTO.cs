using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.DTO
{
    public class MemberFullDetailsDTO
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string ImagePath { get; set; }
        public string HouseAddress { get; set; }
        public string Email { get; set; }
        public DateTime? MembershipDate { get; set; }
        public string Country { get; set; }
        public string Nationality { get; set; }
        public string Profession { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public string EmploymentStatus { get; set; }
        public string MaritalStatus { get; set; }
        public string Permission { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public string MembershipStatus { get; set; }
        public DateTime DeadDate { get; set; }
        public string NextOfKin { get; set; }
        public string RelationshipToNextOfKin { get; set; }
        public string LGA { get; set; }
    }
}
