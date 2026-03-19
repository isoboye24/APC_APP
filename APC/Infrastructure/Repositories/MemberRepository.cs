using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly APCEntities _db;
        public MemberRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.MEMBER.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.MEMBER.First(x => x.memberID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string firstName, string lastName)
        {
            return _db.MEMBER.Any(x => !x.isDeleted && x.name == firstName && x.surname == lastName);
        }

        public List<Member> GetAll()
        {
            var data = _db.MEMBER
                .Where(x => !x.isDeleted)
                .ToList();

            return data.Select(x =>
            {
                var authentication = new MemberAuthentication(
                    x.username,
                    x.password
                );

                var personalInfo = new PersonalInfo(
                    x.name,
                    x.surname,
                    x.birthday,
                    x.imagePath,
                    x.genderID
                );

                var contactInfo = new ContactInfo(
                    x.emailAddress,
                    x.houseAddress,
                    x.phoneNumber,
                    x.phoneNumber2,
                    x.phoneNumber3
                );

                var membershipInfo = new MembershipInfo(
                    x.membershipDate,
                    x.membershipStatusID,
                    x.positionID,
                    x.permissionID
                );

                var demographicInfo = new DemographicInfo(
                    x.countryID,
                    x.nationalityID,
                    x.professionID,
                    x.employmentStatusID,
                    x.maritalStatusID,
                    x.LGAOfCountryOrigin
                );

                var emergencyContact = new EmergencyContact(
                    x.nextOfKin,
                    x.relationshipToKinID
                );

                var lifeStatus = new LifeStatus(
                    x.deadDate
                );

                return Member.Rehydrate(
                    x.memberID,
                    authentication,
                    personalInfo,
                    contactInfo,
                    membershipInfo,
                    demographicInfo,
                    emergencyContact,
                    lifeStatus
                );

            }).ToList();
        }

        public List<MembersBasicDetailDTO> GetAllDeletedMembers()
        {
            var member = (from m in _db.MEMBER.Where(x => x.isDeleted)
                          join g in _db.GENDER on m.genderID equals g.genderID
                          join p in _db.POSITION on m.positionID equals p.positionID
                          join n in _db.NATIONALITY on m.nationalityID equals n.nationalityID
                          select new MembersBasicDetailDTO
                          {
                              MemberId = m.memberID,
                              FirstName = m.name,
                              LastName = m.surname,
                              Nationality = n.nationality1,
                              Position = p.positionName,
                              Gender = g.genderName,
                              ImagePath = m.imagePath,
                          });

            return member.ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.MEMBER.First(x => x.memberID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public Member GetById(int id)
        {
            var entity = _db.MEMBER
                .FirstOrDefault(x => !x.isDeleted && x.memberID == id);

            if (entity == null)
                return null;

            var authentication = new MemberAuthentication(
                entity.username,
                entity.password
            );

            var personalInfo = new PersonalInfo(
                entity.name,
                entity.surname,
                entity.birthday,
                entity.imagePath,
                entity.genderID
            );

            var contactInfo = new ContactInfo(
                entity.emailAddress,
                entity.houseAddress,
                entity.phoneNumber,
                entity.phoneNumber2,
                entity.phoneNumber3
            );

            var membershipInfo = new MembershipInfo(
                entity.membershipDate,
                entity.membershipStatusID,
                entity.positionID,
                entity.permissionID
            );

            var demographicInfo = new DemographicInfo(
                entity.countryID,
                entity.nationalityID,
                entity.professionID,
                entity.employmentStatusID,
                entity.maritalStatusID,
                entity.LGAOfCountryOrigin
            );

            var emergencyContact = new EmergencyContact(
                entity.nextOfKin,
                entity.relationshipToKinID
            );

            var lifeStatus = new LifeStatus(
                entity.deadDate
            );

            return Member.Rehydrate(
                entity.memberID,
                authentication,
                personalInfo,
                contactInfo,
                membershipInfo,
                demographicInfo,
                emergencyContact,
                lifeStatus
            );
        }

        public List<MemberFullDetailsDTO> GetFullMemberDetails()
        {
            var member = (from m in _db.MEMBER.Where(x => x.isDeleted == false)
                          join g in _db.GENDER on m.genderID equals g.genderID
                          join e in _db.EMPLOYMENT_STATUS on m.employmentStatusID equals e.employmentStatusID
                          join p in _db.PROFESSION on m.professionID equals p.professionID
                          join pos in _db.POSITION on m.positionID equals pos.positionID
                          join mar in _db.MARITAL_STATUS on m.maritalStatusID equals mar.maritalStatusID
                          join c in _db.COUNTRY on m.countryID equals c.countryID
                          join n in _db.NATIONALITY on m.nationalityID equals n.nationalityID
                          join perm in _db.PERMISSION on m.permissionID equals perm.permissionID
                          join kin in _db.NEXT_OF_KIN_RELATIONSHIP on m.relationshipToKinID equals kin.RelationshipToKinID
                          join ms in _db.MEMBERSHIP_STATUS on m.membershipStatusID equals ms.membershipStatusID
                          select new MemberFullDetailsDTO
                          {
                              FirstName = m.name,
                              LastName = m.surname,
                              Birthday = m.birthday,
                              ImagePath = m.imagePath,
                              HouseAddress = m.houseAddress,
                              Email = m.emailAddress,
                              MembershipDate = m.membershipDate,
                              Country = c.countryName,
                              Nationality = n.nationality1,
                              Profession = p.profession1,
                              Position = pos.positionName,
                              Gender = g.genderName,
                              EmploymentStatus = e.employmentStatus,
                              MaritalStatus = mar.maritalStatus,
                              PhoneNumber = m.phoneNumber,
                              PhoneNumber2 = m.phoneNumber2,
                              PhoneNumber3 = m.phoneNumber3,
                              MembershipStatus = ms.membershipStatus,
                              DeadDate = m.deadDate,
                              NextOfKin = m.nextOfKin,
                              RelationshipToNextOfKin = kin.RelationshipToKin,
                              LGA = m.LGAOfCountryOrigin,
                          });

            return member.ToList();
        }

        public List<BirthdayMembersDTO> GetBirthdayMembers(int month)
        {
            var member = (from m in _db.MEMBER.Where(x => x.isDeleted == false && x.birthday.Month == month)
                          join g in _db.GENDER on m.genderID equals g.genderID
                          join pos in _db.POSITION on m.positionID equals pos.positionID
                          join ms in _db.MEMBERSHIP_STATUS.Where(x => x.membershipStatus == "Current") on m.membershipStatusID equals ms.membershipStatusID
                          select new BirthdayMembersDTO
                          {
                              MemberId = m.memberID,
                              FirstName = m.name,
                              LastName = m.surname,
                              Birthday = (m.birthday.Day.ToString("00") + "." + m.birthday.Month.ToString("00")).ToString(),
                              ImagePath = m.imagePath,
                              Position = pos.positionName,
                              Gender = g.genderName,
                          });

            return member.ToList();
        }
        
        public List<MembersBasicDetailDTO> GetInactiveMembers()
        {
            var member = (from m in _db.MEMBER.Where(x => x.isDeleted == false)
                          join g in _db.GENDER on m.genderID equals g.genderID
                          join p in _db.POSITION on m.positionID equals p.positionID
                          join n in _db.NATIONALITY on m.nationalityID equals n.nationalityID
                          join ms in _db.MEMBERSHIP_STATUS.Where(x => x.membershipStatus == "Inactive") on m.membershipStatusID equals ms.membershipStatusID
                          select new MembersBasicDetailDTO
                          {
                              MemberId = m.memberID,
                              FirstName = m.name,
                              LastName = m.surname,
                              Nationality = n.nationality1,
                              Position = p.positionName,
                              Gender = g.genderName,
                              ImagePath = m.imagePath,
                          });

            return member.ToList();
        }

        public List<MembersBasicDetailDTO> GetFormerMembers()
        {
            var member = (from m in _db.MEMBER.Where(x => x.isDeleted == false)
                          join g in _db.GENDER on m.genderID equals g.genderID
                          join p in _db.POSITION on m.positionID equals p.positionID
                          join n in _db.NATIONALITY on m.nationalityID equals n.nationalityID
                          join ms in _db.MEMBERSHIP_STATUS.Where(x => x.membershipStatus == "Former") on m.membershipStatusID equals ms.membershipStatusID
                          select new MembersBasicDetailDTO
                          {
                              MemberId = m.memberID,
                              FirstName = m.name,
                              LastName = m.surname,
                              Position = p.positionName,
                              Nationality = n.nationality1,
                              Gender = g.genderName,
                              ImagePath = m.imagePath,
                          });

            return member.ToList();
        }

        public List<DeadMemberShortDetailDTO> GetDeceasedMembers()
        {
            var member = (from m in _db.MEMBER.Where(x => x.isDeleted == false)
                          join g in _db.GENDER on m.genderID equals g.genderID
                          join p in _db.POSITION on m.positionID equals p.positionID
                          join n in _db.NATIONALITY on m.nationalityID equals n.nationalityID
                          join ms in _db.MEMBERSHIP_STATUS.Where(x => x.membershipStatus == "Deceased") on m.membershipStatusID equals ms.membershipStatusID
                          select new DeadMemberShortDetailDTO
                          {
                              MemberId = m.memberID,
                              FirstName = m.name,
                              LastName = m.surname,
                              Birthdate = m.birthday.ToString("dd.MM.yyyy"),
                              Position = p.positionName,
                              Gender = g.genderName,
                              DeadDate = m.deadDate.ToString("dd.MM.yyyy"),
                              Age = (m.deadDate.Year - m.birthday.Year - (m.deadDate < m.birthday.AddYears(m.deadDate.Year - m.birthday.Year) ? 1 : 0)).ToString()
                          });

            return member.ToList();
        }

        public bool Insert(Member data)
        {
            _db.MEMBER.Add(new MEMBER
            {
                surname = data.MemberAuthentication.Username,
                password = data.MemberAuthentication.PasswordHash,

                name = data.PersonalInfo.FirstName,
                username = data.PersonalInfo.LastName,
                birthday = data.PersonalInfo.Birthday,
                imagePath = data.PersonalInfo.ImagePath,
                genderID = data.PersonalInfo.GenderId,

                emailAddress = data.ContactInfo.Email,
                houseAddress = data.ContactInfo.HouseAddress,
                phoneNumber = data.ContactInfo.PhoneNumber,
                phoneNumber2 = data.ContactInfo.PhoneNumber2,
                phoneNumber3 = data.ContactInfo.PhoneNumber3,

                membershipDate = data.MembershipInfo.MembershipDate,
                membershipStatusID = data.MembershipInfo.MembershipStatusId,
                positionID = data.MembershipInfo.PositionId,
                permissionID = data.MembershipInfo.PermissionId,

                countryID = data.DemographicInfo.CountryId,
                nationalityID = data.DemographicInfo.NationalityId,
                professionID = data.DemographicInfo.ProfessionId,
                employmentStatusID = data.DemographicInfo.EmploymentStatusId,
                maritalStatusID = data.DemographicInfo.MaritalStatusId,
                LGAOfCountryOrigin = data.DemographicInfo.LGA,

                nextOfKin = data.EmergencyContact.NextOfKin,
                relationshipToKinID = data.EmergencyContact.RelationshipToNextOfKinId,

                deadDate = data.LifeStatus.DeadDate,
                
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.MEMBER.FirstOrDefault(x => x.memberID == id);

            if (entity == null)
                return false;

            _db.MEMBER.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Member data)
        {
            var entity = _db.MEMBER.First(x => x.memberID == data.MemberId);

            entity.surname = data.MemberAuthentication.Username;
            entity.password = data.MemberAuthentication.PasswordHash;

            entity.name = data.PersonalInfo.FirstName;
            entity.username = data.PersonalInfo.LastName;
            entity.birthday = data.PersonalInfo.Birthday;
            entity.imagePath = data.PersonalInfo.ImagePath;
            entity.genderID = data.PersonalInfo.GenderId;

            entity.emailAddress = data.ContactInfo.Email;
            entity.houseAddress = data.ContactInfo.HouseAddress;
            entity.phoneNumber = data.ContactInfo.PhoneNumber;
            entity.phoneNumber2 = data.ContactInfo.PhoneNumber2;
            entity.phoneNumber3 = data.ContactInfo.PhoneNumber3;

            entity.membershipDate = data.MembershipInfo.MembershipDate;
            entity.membershipStatusID = data.MembershipInfo.MembershipStatusId;
            entity.positionID = data.MembershipInfo.PositionId;
            entity.permissionID = data.MembershipInfo.PermissionId;

            entity.countryID = data.DemographicInfo.CountryId;
            entity.nationalityID = data.DemographicInfo.NationalityId;
            entity.professionID = data.DemographicInfo.ProfessionId;
            entity.employmentStatusID = data.DemographicInfo.EmploymentStatusId;
            entity.maritalStatusID = data.DemographicInfo.MaritalStatusId;
            entity.LGAOfCountryOrigin = data.DemographicInfo.LGA;

            entity.nextOfKin = data.EmergencyContact.NextOfKin;
            entity.relationshipToKinID = data.EmergencyContact.RelationshipToNextOfKinId;

            entity.deadDate = data.LifeStatus.DeadDate;           

            _db.SaveChanges();
            return true;
        }

        public MEMBER GetByUsername(string username)
        {
            return _db.MEMBER.FirstOrDefault(x => !x.isDeleted && x.username == username);
        }

        public int Get3MonthsAbsentesCount()
        {
            var last3MeetingIds = _db.GENERAL_ATTENDANCE
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .ThenByDescending(x => x.monthID)
                .ThenByDescending(x => x.day)
                .Select(x => x.generalAttendanceID)
                .Take(3);

            var count = _db.PERSONAL_ATTENDANCE
                .Where(x => !x.isDeleted && last3MeetingIds.Contains(x.generalAttendanceID))
                .GroupBy(x => x.memberID)
                .Where(g => g.Count() == 3)
                .Count();

            return count;
        }
    }
}
