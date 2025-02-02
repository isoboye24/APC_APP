using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace APC.DAL.DAO
{
    public class MemberDAO : APCContexts, IDAO<MemberDetailDTO, MEMBER>
    {
        public List<MEMBER> CheckMember(string password, string username)
        {
            try
            {
                List<MEMBER> list = db.MEMBERs.Where(x => x.username == username && x.password == password && x.membershipStatusID == 1).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public bool Delete(MEMBER entity)
        {            
            try
            {
                MEMBER member = db.MEMBERs.First(x => x.memberID == entity.memberID);
                member.isDeleted = true;
                member.deletedDate = DateTime.Today;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeletePermission(MEMBER entity)
        {
            try
            {
                MEMBER member = db.MEMBERs.First(x => x.memberID == entity.memberID);
                member.permissionID = 2;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool GetBack(int ID)
        {
            try
            {
                MEMBER member = db.MEMBERs.First(x=>x.memberID==ID);
                member.isDeleted = false;
                member.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(MEMBER entity)
        {
            try
            {
                db.MEMBERs.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public List<MemberDetailDTO> Select()
        {
            try
            {
                List<MemberDetailDTO> members = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x=>x.isDeleted==false)
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join e in db.EMPLOYMENT_STATUS.Where(x => x.isDeleted == false) on m.employmentStatusID equals e.employmentStatusID
                            join p in db.PROFESSIONs.Where(x => x.isDeleted == false) on m.professionID equals p.professionID
                            join pos in db.POSITIONs.Where(x => x.isDeleted == false) on m.positionID equals pos.positionID
                            join mar in db.MARITAL_STATUS.Where(x => x.isDeleted == false) on m.maritalStatusID equals mar.maritalStatusID
                            join c in db.COUNTRies.Where(x => x.isDeleted == false) on m.countryID equals c.countryID
                            join n in db.NATIONALITies.Where(x => x.isDeleted == false) on m.nationalityID equals n.nationalityID
                            join perm in db.PERMISSIONs.Where(x => x.isDeleted == false) on m.permissionID equals perm.permissionID
                            join kin in db.NEXT_OF_KIN_RELATIONSHIP on m.relationshipToKinID equals kin.RelationshipToKinID
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
                                LGA = m.LGAOfCountryOrigin,
                                nameOfNextOfKin = m.nextOfKin,
                                relationshipToKinID = m.relationshipToKinID,
                                relationshipToKin = kin.RelationshipToKin,
                            }).OrderBy(x=>x.surname).ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
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
                    dto.LGA = item.LGA;
                    dto.MembershipStatusID = item.membershipStatusID;
                    dto.MembershipStatus = item.membershipStatus;
                    dto.NameOfNextOfKin = item.nameOfNextOfKin;
                    dto.RelationshipToKinID = item.relationshipToKinID;
                    dto.RelationshipToKin = item.relationshipToKin;
                    members.Add(dto);
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        public List<MemberDetailDTO> SelectFormerMembers()
        {
            try
            {
                List<MemberDetailDTO> members = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x => x.isDeleted == false)
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join e in db.EMPLOYMENT_STATUS.Where(x => x.isDeleted == false) on m.employmentStatusID equals e.employmentStatusID
                            join p in db.PROFESSIONs.Where(x => x.isDeleted == false) on m.professionID equals p.professionID
                            join pos in db.POSITIONs.Where(x => x.isDeleted == false) on m.positionID equals pos.positionID
                            join mar in db.MARITAL_STATUS.Where(x => x.isDeleted == false) on m.maritalStatusID equals mar.maritalStatusID
                            join c in db.COUNTRies.Where(x => x.isDeleted == false) on m.countryID equals c.countryID
                            join n in db.NATIONALITies.Where(x => x.isDeleted == false) on m.nationalityID equals n.nationalityID
                            join perm in db.PERMISSIONs.Where(x => x.isDeleted == false) on m.permissionID equals perm.permissionID
                            join kin in db.NEXT_OF_KIN_RELATIONSHIP on m.relationshipToKinID equals kin.RelationshipToKinID
                            join ms in db.MEMBERSHIP_STATUS.Where(x => x.membershipStatus == "Former") on m.membershipStatusID equals ms.membershipStatusID
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
                                LGA = m.LGAOfCountryOrigin,
                                membershipStatusID = m.membershipStatusID,
                                membershipStatus = ms.membershipStatus,
                                nameOfNextOfKin = m.nextOfKin,
                                relationshipToKinID = m.relationshipToKinID,
                                relationshipToKin = kin.RelationshipToKin,
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
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
                    dto.LGA = item.LGA;
                    dto.MembershipStatusID = item.membershipStatusID;
                    dto.MembershipStatus = item.membershipStatus;
                    dto.NameOfNextOfKin = item.nameOfNextOfKin;
                    dto.RelationshipToKinID = item.relationshipToKinID;
                    dto.RelationshipToKin = item.relationshipToKin;
                    members.Add(dto);
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MemberDetailDTO> SelectDeadMembers()
        {
            try
            {
                List<MemberDetailDTO> members = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x => x.isDeleted == false)
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join e in db.EMPLOYMENT_STATUS.Where(x => x.isDeleted == false) on m.employmentStatusID equals e.employmentStatusID
                            join p in db.PROFESSIONs.Where(x => x.isDeleted == false) on m.professionID equals p.professionID
                            join pos in db.POSITIONs.Where(x => x.isDeleted == false) on m.positionID equals pos.positionID
                            join mar in db.MARITAL_STATUS.Where(x => x.isDeleted == false) on m.maritalStatusID equals mar.maritalStatusID
                            join c in db.COUNTRies.Where(x => x.isDeleted == false) on m.countryID equals c.countryID
                            join n in db.NATIONALITies.Where(x => x.isDeleted == false) on m.nationalityID equals n.nationalityID
                            join perm in db.PERMISSIONs.Where(x => x.isDeleted == false) on m.permissionID equals perm.permissionID
                            join kin in db.NEXT_OF_KIN_RELATIONSHIP on m.relationshipToKinID equals kin.RelationshipToKinID
                            join ms in db.MEMBERSHIP_STATUS.Where(x=>x.membershipStatus=="Deceased") on m.membershipStatusID equals ms.membershipStatusID
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
                                LGA = m.LGAOfCountryOrigin,
                                nameOfNextOfKin = m.nextOfKin,
                                relationshipToKinID = m.relationshipToKinID,
                                relationshipToKin = kin.RelationshipToKin,
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
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
                    dto.MembershipStatusID = item.membershipStatusID;
                    dto.MembershipStatus = item.membershipStatus;
                    dto.DeadDate = item.deadDate;
                    dto.LGA = item.LGA;
                    dto.NameOfNextOfKin = item.nameOfNextOfKin;
                    dto.RelationshipToKinID = item.relationshipToKinID;
                    dto.RelationshipToKin = item.relationshipToKin;
                    TimeSpan difference = dto.DeadDate - dto.Birthday;
                    dto.DeadAge = Math.Floor(difference.TotalDays / 365.25);
                    members.Add(dto);
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MemberDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<MemberDetailDTO> members = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x => x.isDeleted == isDeleted && x.membershipStatusID == 1)
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join e in db.EMPLOYMENT_STATUS on m.employmentStatusID equals e.employmentStatusID
                            join p in db.PROFESSIONs on m.professionID equals p.professionID
                            join pos in db.POSITIONs on m.positionID equals pos.positionID
                            join mar in db.MARITAL_STATUS on m.maritalStatusID equals mar.maritalStatusID
                            join c in db.COUNTRies on m.countryID equals c.countryID
                            join n in db.NATIONALITies on m.nationalityID equals n.nationalityID
                            join perm in db.PERMISSIONs on m.permissionID equals perm.permissionID
                            join kin in db.NEXT_OF_KIN_RELATIONSHIP on m.relationshipToKinID equals kin.RelationshipToKinID
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
                                isCountryDeleted = c.isDeleted,
                                isNationalityDeleted = n.isDeleted,
                                isProfessionDeleted = p.isDeleted,
                                isPositionDeleted = pos.isDeleted,
                                isEmpStatusDeleted = e.isDeleted,
                                isMarStatusDeleted = mar.isDeleted,
                                deadDate = m.deadDate,
                                LGA = m.LGAOfCountryOrigin,
                                membershipStatusID = m.membershipStatusID,
                                membershipStatus = ms.membershipStatus,
                                nameOfNextOfKin = m.nextOfKin,
                                relationshipToKinID = m.relationshipToKinID,
                                relationshipToKin = kin.RelationshipToKin,
                            }).OrderBy(x => x.surname).ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
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
                    dto.isCountryDeleted = item.isCountryDeleted;
                    dto.isNationalityDeleted = item.isNationalityDeleted;
                    dto.isProfessionDeleted = item.isProfessionDeleted;
                    dto.isPositionDeleted = item.isPositionDeleted;
                    dto.isEmpStatusDeleted = item.isEmpStatusDeleted;
                    dto.isMarStatusDeleted = item.isMarStatusDeleted;
                    dto.DeadDate = item.deadDate;
                    dto.LGA = item.LGA;
                    dto.MembershipStatusID = item.membershipStatusID;
                    dto.MembershipStatus = item.membershipStatus;
                    dto.NameOfNextOfKin = item.nameOfNextOfKin;
                    dto.RelationshipToKinID = item.relationshipToKinID;
                    dto.RelationshipToKin = item.relationshipToKin;
                    members.Add(dto);
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectAllMembersCount()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.membershipStatusID == 1);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountMale()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x=>x.isDeleted==false && x.genderID==1 && x.membershipStatusID == 1);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountDeadMale()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.genderID == 1 && x.membershipStatusID == 3);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountFormerMale()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.genderID == 1 && x.membershipStatusID == 2);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetLastMemberUsername()
        {            
            try
            {
                string username;
                var lastMemberUsername = db.MEMBERs.OrderByDescending(x=>x.memberID).FirstOrDefault();
                if (lastMemberUsername != null)
                {
                    username = lastMemberUsername.username;
                    return username;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountFemale()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.genderID == 2 && x.membershipStatusID == 1);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountDeadFemale()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.genderID == 2 && x.membershipStatusID == 3);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountFormerFemale()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.genderID == 2 && x.membershipStatusID == 2);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountDivisor()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.genderID == 3 && x.membershipStatusID == 1);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountDeadDivisor()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.genderID == 3 && x.membershipStatusID == 3);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountFormerDivisor()
        {
            try
            {
                int numberOfMembers = db.MEMBERs.Count(x => x.isDeleted == false && x.genderID == 3 && x.membershipStatusID == 2);
                return numberOfMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectCountUniqueNationality()
        {
            try
            {
                List<string> nationalityList = new List<string>();
                List<MemberDetailDTO> uniqueNationality = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x => x.isDeleted == false && x.membershipStatusID == 1)
                            join n in db.NATIONALITies.Where(x => x.isDeleted == false) on m.nationalityID equals n.nationalityID
                            select new
                            {
                                memberID = m.memberID,
                                nationalityID = m.nationalityID,
                                nationality = n.nationality1,
                            }).Distinct().ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
                    dto.NationalityName = item.nationality;
                    nationalityList.Add(item.nationality);
                    uniqueNationality.Add(dto);
                }
                var getUniqueList = nationalityList.Distinct().ToList();
                int nationalityCount = getUniqueList.Count();
                return nationalityCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectUniquePositionCount()
        {
            try
            {
                List<string> positionList = new List<string>();
                List<MemberDetailDTO> uniquePosition = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x => x.isDeleted == false && x.membershipStatusID == 1)
                            join p in db.POSITIONs.Where(x => x.isDeleted == false) on m.positionID equals p.positionID
                            select new
                            {
                                memberID = m.memberID,
                                positionID = m.positionID,
                                position = p.positionName,
                            }).Distinct().ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
                    dto.PositionName = item.position;
                    positionList.Add(item.position);
                    uniquePosition.Add(dto);
                }
                var getUniqueList = positionList.Distinct().ToList();
                int positionCount = getUniqueList.Count();
                return positionCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectUniqueProfessionCount()
        {
            try
            {
                List<string> professionList = new List<string>();
                List<MemberDetailDTO> uniqueProfession = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x => x.isDeleted == false && x.membershipStatusID == 1)
                            join p in db.PROFESSIONs.Where(x => x.isDeleted == false) on m.professionID equals p.professionID
                            select new
                            {
                                memberID = m.memberID,
                                professionID = m.professionID,
                                profession = p.profession1,
                            }).Distinct().ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
                    dto.PositionName = item.profession;
                    professionList.Add(item.profession);
                    uniqueProfession.Add(dto);
                }
                var getUniqueList = professionList.Distinct().ToList();
                int professionCount = getUniqueList.Count();
                return professionCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MemberDetailDTO> SelectMembersWithAccess()
        {
            try
            {
                List<MemberDetailDTO> members = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x => x.isDeleted == false && x.membershipStatusID == 1)
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join e in db.EMPLOYMENT_STATUS on m.employmentStatusID equals e.employmentStatusID
                            join p in db.PROFESSIONs on m.professionID equals p.professionID
                            join pos in db.POSITIONs on m.positionID equals pos.positionID
                            join mar in db.MARITAL_STATUS on m.maritalStatusID equals mar.maritalStatusID
                            join c in db.COUNTRies on m.countryID equals c.countryID
                            join n in db.NATIONALITies on m.nationalityID equals n.nationalityID
                            join perm in db.PERMISSIONs.Where(x=>x.permission1 != "Member") on m.permissionID equals perm.permissionID
                            //join kin in db.NEXT_OF_KIN_RELATIONSHIP on m.relationshipToKinID equals kin.ID
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
                                LGA = m.LGAOfCountryOrigin,
                                membershipStatusID = m.membershipStatusID,
                                membershipStatus = ms.membershipStatus,
                                //nextOfKin = m.nextOfKin,
                                //relationshipToNextOfKinID = m.relationshipToKinID,
                                //relationshipToNextOfKin = kin.RelationToNextOfKin,
                            }).OrderBy(x => x.permissionName).ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
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
                    dto.LGA = item.LGA;
                    dto.MembershipStatusID = item.membershipStatusID;
                    dto.MembershipStatus = item.membershipStatus;
                    //dto.NextOfKin = item.nextOfKin;
                    //dto.RelationshipToNextOfKinID = item.relationshipToNextOfKinID;
                    //dto.RelationshipToNextOfKin = item.relationshipToNextOfKin;
                    members.Add(dto);
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MemberDetailDTO> SelectSpecificMember(int ID)
        {
            try
            {
                List<MemberDetailDTO> members = new List<MemberDetailDTO>();
                var list = (from m in db.MEMBERs.Where(x => x.isDeleted == false && x.memberID == ID)
                            join g in db.GENDERs on m.genderID equals g.genderID
                            join e in db.EMPLOYMENT_STATUS on m.employmentStatusID equals e.employmentStatusID
                            join p in db.PROFESSIONs on m.professionID equals p.professionID
                            join pos in db.POSITIONs on m.positionID equals pos.positionID
                            join mar in db.MARITAL_STATUS on m.maritalStatusID equals mar.maritalStatusID
                            join c in db.COUNTRies on m.countryID equals c.countryID
                            join n in db.NATIONALITies on m.nationalityID equals n.nationalityID
                            join perm in db.PERMISSIONs on m.permissionID equals perm.permissionID
                            //join kin in db.NEXT_OF_KIN_RELATIONSHIP on m.relationshipToKinID equals kin.ID
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
                                LGA = m.LGAOfCountryOrigin,
                                membershipStatusID = m.membershipStatusID,
                                membershipStatus = ms.membershipStatus,
                                //nextOfKin = m.nextOfKin,
                                //relationshipToNextOfKinID = m.relationshipToKinID,
                                //relationshipToNextOfKin = kin.RelationToNextOfKin,
                            }).OrderBy(x => x.permissionName).ToList();
                foreach (var item in list)
                {
                    MemberDetailDTO dto = new MemberDetailDTO();
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
                    dto.LGA = item.LGA;
                    dto.MembershipStatusID = item.membershipStatusID;
                    dto.MembershipStatus = item.membershipStatus;
                    //dto.NextOfKin = item.nextOfKin;
                    //dto.RelationshipToNextOfKinID = item.relationshipToNextOfKinID;
                    //dto.RelationshipToNextOfKin = item.relationshipToNextOfKin;
                    members.Add(dto);
                }
                return members;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectPermittedMembersCount()
        {
            try
            {
                int permittedMembers = db.MEMBERs.Count(x => x.permissionID > 2 && x.membershipStatusID == 1);
                return permittedMembers;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        public List<AbsenteesDetailDTO> Select3MonthsAbsentes()
        {
            List<AbsenteesDetailDTO> absentees = new List<AbsenteesDetailDTO>();
            List<int> members = new List<int>();
            List<int> absentMembers = new List<int>();
            List<string> absentCheckList = new List<string>();
            var Absentees = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false)
                            join mem in db.MEMBERs.Where(x => x.isDeleted == false) on p.memberID equals mem.memberID
                            join ats in db.ATTENDANCE_STATUS.Where(x => x.attendanceStatus == "Absent") on p.attendanceStatusID equals ats.attendanceStatusID
                            join gen in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ThenByDescending(x=>x.monthID).Take(3) on p.generalAttendanceID equals gen.generalAttendanceID
                            select new
                            {
                                attendanceID = p.attendanceID,
                                memberID = mem.memberID,
                            }).ToList();
            foreach (var item in Absentees)
            {
                members.Add(item.memberID);
            }
            var membersAppearingThreeTimes = General.FindMembersAppearingThreeTimes(members);
            if (membersAppearingThreeTimes.Any())
            {
                foreach (var memberID in membersAppearingThreeTimes)
                {
                    var list = (from m in db.MEMBERs.Where(x => x.isDeleted == false && x.memberID == memberID)
                                join g in db.GENDERs on m.genderID equals g.genderID
                                join pos in db.POSITIONs on m.positionID equals pos.positionID
                                join ms in db.MEMBERSHIP_STATUS.Where(x => x.membershipStatus == "Current") on m.membershipStatusID equals ms.membershipStatusID
                                select new
                                {
                                    memberID = m.memberID,
                                    name = m.name,
                                    surname = m.surname,
                                    imagePath = m.imagePath,
                                    positionID = m.positionID,
                                    positionName = pos.positionName,
                                    genderID = m.genderID,
                                    genderName = g.genderName,
                                    membershipStatusID = m.membershipStatusID,
                                    membershipStatus = ms.membershipStatus,
                                }).OrderBy(x => x.surname).ToList();
                    foreach (var item2 in list)
                    {
                        AbsenteesDetailDTO dto = new AbsenteesDetailDTO();
                        dto.MemberID = item2.memberID;
                        dto.Name = item2.name;
                        dto.Surname = item2.surname;
                        dto.ImagePath = item2.imagePath;
                        dto.PositionID = item2.positionID;
                        dto.PositionName = item2.positionName;
                        dto.GenderID = item2.genderID;
                        dto.GenderName = item2.genderName;
                        absentees.Add(dto);
                    }
                }
            }
            return absentees;
        }

        public int Select3MonthsAbsentesCount()
        {
            List<AbsenteesDetailDTO> absentees = new List<AbsenteesDetailDTO>();
            List<int> members = new List<int>();
            List<int> absentMembers = new List<int>();
            List<string> absentCheckList = new List<string>();

            var last3Meetings = db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ThenByDescending(x => x.day).Take(3).ToList();
            foreach (var meeting in last3Meetings)
            {
                var allAbsentMembers = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.generalAttendanceID == meeting.generalAttendanceID).ToList();
                foreach (var member in allAbsentMembers)
                {
                    members.Add(member.memberID);
                }
            }
            var membersAppearingThreeTimes = General.FindMembersAppearingThreeTimes(members);
            int memberCount = membersAppearingThreeTimes.Count();
            if (memberCount > 0)
            {
                return memberCount;
            }
            else
            {
                return 0;
            }

            //var Absentees = (from p in db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false)
            //                 join mem in db.MEMBERs.Where(x => x.isDeleted == false) on p.memberID equals mem.memberID
            //                 join ats in db.ATTENDANCE_STATUS.Where(x => x.attendanceStatus == "Absent") on p.attendanceStatusID equals ats.attendanceStatusID
            //                 join gen in db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false).OrderByDescending(x => x.year).ThenByDescending(x => x.monthID).ThenByDescending(x => x.day).Take(3) on p.generalAttendanceID equals gen.generalAttendanceID
            //                 select new
            //                 {
            //                     attendanceID = p.attendanceID,
            //                     memberID = mem.memberID,
            //                 }).ToList();
            //foreach (var item in Absentees)
            //{
            //    members.Add(item.memberID);
            //}

        }
        public int GetNoOfMembersPresentAttendance(int ID)
        {
            try
            {
                int NoOfPresent = db.PERSONAL_ATTENDANCE.Count(x => x.memberID == ID && x.isDeleted == false && x.attendanceStatusID == 2);
                if (NoOfPresent != 0)
                {
                    return NoOfPresent;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNoOfMembersAbsentAttendance(int ID)
        {
            try
            {
                int NoOfAbsent = db.PERSONAL_ATTENDANCE.Count(x => x.memberID == ID && x.isDeleted == false && x.attendanceStatusID == 3);
                if (NoOfAbsent != 0)
                {
                    return NoOfAbsent;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal GetAmountContributed(int ID)
        {
            try
            {
                List<decimal> totalAmount = new List<decimal>();
                int NoPresent = db.PERSONAL_ATTENDANCE.Count(x => x.memberID == ID && x.isDeleted == false && x.attendanceStatusID == 2);
                if (NoPresent != 0)
                {
                    var amountContributed = db.PERSONAL_ATTENDANCE.Where(x => x.memberID == ID && x.isDeleted == false && x.monthlyDues > 0).ToList();
                    foreach (var item in amountContributed)
                    {
                        totalAmount.Add((decimal)item.monthlyDues);
                    }
                    decimal totalAmountContributed = totalAmount.Sum();
                    return totalAmountContributed;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal GetAmountExpected(int ID)
        {
            try
            {
                var memberInfo = db.MEMBERs.FirstOrDefault(x => x.memberID == ID && x.isDeleted == false);
                decimal totalAmountExpected;
                List<int> meetingCount = new List<int>();

                if (memberInfo != null)
                {
                    DateTime membershipDate = (DateTime)memberInfo.membershipDate;

                    int meeting = (from p in db.PERSONAL_ATTENDANCE
                                   join g in db.GENERAL_ATTENDANCE on p.generalAttendanceID equals g.generalAttendanceID
                                   join m in db.MEMBERs on p.memberID equals m.memberID
                                   where m.memberID == ID && g.isDeleted == false && g.attendanceDate > membershipDate
                                   select p).Count();
                    if (meeting != 0)
                    {
                        meetingCount.Add(meeting);                        
                    }                    
                }
                decimal feePerMeeting = 10.0m;
                totalAmountExpected = feePerMeeting * meetingCount.Sum();
                return totalAmountExpected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(MEMBER entity)
        {
            try
            {
                MEMBER member = db.MEMBERs.First(x => x.memberID == entity.memberID);
                member.surname = entity.surname;
                member.name = entity.name;
                member.username = entity.username;
                member.password = entity.password;
                member.birthday = entity.birthday;
                member.imagePath = entity.imagePath;
                member.houseAddress = entity.houseAddress;
                member.emailAddress = entity.emailAddress;
                member.membershipDate = entity.membershipDate;
                member.countryID = entity.countryID;
                member.nationalityID = entity.nationalityID;
                member.professionID = entity.professionID;
                member.positionID = entity.positionID;
                member.genderID = entity.genderID;
                member.employmentStatusID = entity.employmentStatusID;
                member.maritalStatusID = entity.maritalStatusID;
                member.permissionID = entity.permissionID;
                member.phoneNumber = entity.phoneNumber;
                member.phoneNumber2 = entity.phoneNumber2;
                member.phoneNumber3 = entity.phoneNumber3;
                member.deadDate = entity.deadDate;
                member.LGAOfCountryOrigin = entity.LGAOfCountryOrigin;
                member.membershipStatusID = entity.membershipStatusID;
                member.nextOfKin = entity.nextOfKin;
                member.LGAOfCountryOrigin = entity.LGAOfCountryOrigin;
                member.relationshipToKinID = entity.relationshipToKinID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
