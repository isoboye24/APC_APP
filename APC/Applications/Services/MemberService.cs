using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        public MemberService(IMemberRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public AuthenticationDTO Authenticate(string username, string password)
        {
            var member = _repository.GetByUsername(username);

            if (member == null || member.password != password)
                return null;

            _currentUserService.SetUser(member.memberID, member.username, member.permissionID);

            return new AuthenticationDTO
            {
                MemberId = member.memberID,
                Username = member.username,
                AccessLevel = member.permissionID
            };
        }

        public int Count()
            => _repository.Count();

        public bool Create(Member data)
        {
            if (_repository.Exists(data.PersonalInfo.FirstName, data.PersonalInfo.LastName))
                throw new Exception("Member already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

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


        public List<MembersBasicDetailDTO> GetAllDeletedMembers()
            => _repository.GetAllDeletedMembers();

        public bool GetBack(int id)
            => _repository.GetBack(id);


        public List<MemberFullDetailsDTO> GetFullMemberDetails()
            => _repository.GetFullMemberDetails();

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public int Get3MonthsAbsentesCount()
            => _repository.Get3MonthsAbsentesCount();

        public bool Update(Member data)
        {
            var member = _repository.GetById(data.MemberId);
            if (member == null)
                throw new InvalidOperationException("Member not found");

            member.MemberAuthentication.UpdateUsername(data.MemberAuthentication.Username);
            member.MemberAuthentication.UpdatePasswordHash(data.MemberAuthentication.PasswordHash);

            member.PersonalInfo.UpdateFirstName(data.PersonalInfo.FirstName);
            member.PersonalInfo.UpdateLastName(data.PersonalInfo.LastName);
            member.PersonalInfo.UpdateBirthday(data.PersonalInfo.Birthday);
            member.PersonalInfo.UpdateImagePath(data.PersonalInfo.ImagePath);
            member.PersonalInfo.UpdateGender(data.PersonalInfo.GenderId);

            member.ContactInfo.UpdateEmail(data.ContactInfo.Email);
            member.ContactInfo.UpdateHouseAddress(data.ContactInfo.HouseAddress);
            member.ContactInfo.UpdatePhoneNumber(data.ContactInfo.PhoneNumber);
            member.ContactInfo.UpdatePhoneNumber2(data.ContactInfo.PhoneNumber2);
            member.ContactInfo.UpdatePhoneNumber3(data.ContactInfo.PhoneNumber3);

            member.MembershipInfo.UpdateMembershipDate((DateTime)data.MembershipInfo.MembershipDate);
            member.MembershipInfo.UpdateMembershipStatus(data.MembershipInfo.MembershipStatusId);
            member.MembershipInfo.UpdatePosition(data.MembershipInfo.PositionId);
            member.MembershipInfo.UpdatePermission(data.MembershipInfo.PermissionId);

            member.DemographicInfo.UpdateCountry(data.DemographicInfo.CountryId);
            member.DemographicInfo.UpdateNationality(data.DemographicInfo.NationalityId);
            member.DemographicInfo.UpdateProfession(data.DemographicInfo.ProfessionId);
            member.DemographicInfo.UpdateEmplomentStatus(data.DemographicInfo.EmploymentStatusId);
            member.DemographicInfo.UpdateMaritalStatus(data.DemographicInfo.MaritalStatusId);
            member.DemographicInfo.UpdateLGA(data.DemographicInfo.LGA);

            member.EmergencyContact.UpdateNextOfKin(data.EmergencyContact.NextOfKin);
            member.EmergencyContact.UpdateRelationshipToNextOfKin(data.EmergencyContact.RelationshipToNextOfKinId);

            member.LifeStatus.UpdateDeadDate(data.LifeStatus.DeadDate);

            return _repository.Update(member);
        }
        public int GetUniqueProfessionCount()
            => _repository.GetUniqueProfessionCount();

        public int GetUniquePositionCount()
            => _repository.GetUniquePositionCount();
        
        public int GetUniqueNationalityCount()
            => _repository.GetUniqueNationalityCount();
        
        public int GetUniquePermissionCount()
            => _repository.GetUniquePermissionCount();


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

        public int GetUniqueProfessionCount()
        {
            return (from m in _db.MEMBER
                    join ms in _db.MEMBERSHIP_STATUS on m.membershipStatusID equals ms.membershipStatusID
                    join p in _db.PROFESSION on m.professionID equals p.professionID
                    where !m.isDeleted
                          && ms.membershipStatus == "Current"
                          && !p.isDeleted
                    select p.profession1)
                    .Distinct()
                    .Count();
        }

        public int GetUniquePositionCount()
        {
            return (from m in _db.MEMBER
                    join ms in _db.MEMBERSHIP_STATUS on m.membershipStatusID equals ms.membershipStatusID
                    join p in _db.POSITION on m.positionID equals p.positionID
                    where !m.isDeleted
                          && ms.membershipStatus == "Current"
                          && !p.isDeleted
                    select p.positionName)
                    .Distinct()
                    .Count();
        }

        public int GetUniqueNationalityCount()
        {
            return (from m in _db.MEMBER
                    join ms in _db.MEMBERSHIP_STATUS on m.membershipStatusID equals ms.membershipStatusID
                    join n in _db.NATIONALITY on m.nationalityID equals n.nationalityID
                    where !m.isDeleted
                          && ms.membershipStatus == "Current"
                          && !n.isDeleted
                    select n.nationality1)
                    .Distinct()
                    .Count();
        }

        public int GetUniquePermissionCount()
        {
            return (from m in _db.MEMBER
                    join ms in _db.MEMBERSHIP_STATUS on m.membershipStatusID equals ms.membershipStatusID
                    join p in _db.PERMISSION on m.permissionID equals p.permissionID
                    where !m.isDeleted
                          && ms.membershipStatus == "Current"
                          && !p.isDeleted
                    select p.permission1)
                    .Distinct()
                    .Count();
        }
    }
}
