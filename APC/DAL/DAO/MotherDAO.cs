using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class MotherDAO:APCContexts
    {
        public List<MothersDetailDTO> Select()
        {
            try
            {
                List<MothersDetailDTO> mothers = new List<MothersDetailDTO>();
                var list = (from m in db.MEMBER.Where(x => x.isDeleted == false)
                            join g in db.GENDER.Where(x => x.genderName == "Female") on m.genderID equals g.genderID
                            join e in db.EMPLOYMENT_STATUS.Where(x => x.isDeleted == false) on m.employmentStatusID equals e.employmentStatusID
                            join p in db.PROFESSION.Where(x => x.isDeleted == false) on m.professionID equals p.professionID
                            join pos in db.POSITION.Where(x => x.isDeleted == false) on m.positionID equals pos.positionID
                            join mar in db.MARITAL_STATUS.Where(x => x.isDeleted == false) on m.maritalStatusID equals mar.maritalStatusID
                            join c in db.COUNTRY.Where(x => x.isDeleted == false) on m.countryID equals c.countryID
                            join n in db.NATIONALITY.Where(x => x.isDeleted == false) on m.nationalityID equals n.nationalityID
                            join perm in db.PERMISSION.Where(x => x.isDeleted == false) on m.permissionID equals perm.permissionID
                            join ms in db.MEMBERSHIP_STATUS.Where(x => x.membershipStatus == "Current") on m.membershipStatusID equals ms.membershipStatusID
                            select new
                            {
                                memberID = m.memberID,
                                username = m.username,
                                name = m.name,
                                surname = m.surname,
                                password = m.password,
                                birthday = m.birthday,
                                imagePath = m.imagePath,
                                emailAddress = m.emailAddress,
                                houseAddress = m.houseAddress,
                                membershipDate = m.membershipDate,
                                countryID = m.countryID,
                                countryName = c.countryName,
                                nationalityID = m.nationalityID,
                                nationalityName = n.nationality1,
                                professionID = m.professionID,
                                professionName = p.profession1,
                                positionID = m.positionID,
                                positionName = pos.positionName,
                                genderID = m.genderID,
                                genderName = g.genderName,
                                employmenStatusID = m.employmentStatusID,
                                employmenStatusName = e.employmentStatus,
                                maritalStatusID = m.maritalStatusID,
                                maritalStatusName = mar.maritalStatus,
                                permissionID = m.permissionID,
                                permissionName = perm.permission1,
                                phoneNumber = m.phoneNumber,
                                phoneNumber2 = m.phoneNumber2,
                                phoneNumber3 = m.phoneNumber3,
                                deadDate = m.deadDate,
                                membershipStatusID = m.membershipStatusID,
                                membershipStatus = ms.membershipStatus,
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    MothersDetailDTO dto = new MothersDetailDTO();
                    dto.MemberID = item.memberID;
                    dto.Username = item.username;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.Password = item.password;
                    dto.Birthday = item.birthday;
                    dto.ImagePath = item.imagePath;
                    dto.EmailAddress = item.emailAddress;
                    dto.HouseAddress = item.houseAddress;
                    dto.MembershipDate = (DateTime)item.membershipDate;
                    dto.CountryID = item.countryID;
                    dto.CountryName = item.countryName;
                    dto.NationalityID = item.nationalityID;
                    dto.NationalityName = item.nationalityName;
                    dto.ProfessionID = item.professionID;
                    dto.ProfessionName = item.professionName;
                    dto.PositionID = item.positionID;
                    dto.PositionName = item.positionName;
                    dto.GenderID = item.genderID;
                    dto.GenderName = item.genderName;
                    dto.EmploymentStatusID = item.employmenStatusID;
                    dto.EmploymentStatusName = item.employmenStatusName;
                    dto.MaritalStatusID = item.maritalStatusID;
                    dto.MaritalStatusName = item.maritalStatusName;
                    dto.PermissionID = item.permissionID;
                    dto.PermissionName = item.permissionName;
                    dto.PhoneNumber = item.phoneNumber;
                    dto.PhoneNumber2 = item.phoneNumber2;
                    dto.PhoneNumber3 = item.phoneNumber3;
                    dto.DeadDate = item.deadDate;
                    dto.MembershipStatusID = item.membershipStatusID;
                    dto.MembershipStatus = item.membershipStatus;
                    mothers.Add(dto);
                }
                return mothers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
