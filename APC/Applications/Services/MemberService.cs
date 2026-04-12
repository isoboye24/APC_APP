using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using APC.Helper;

namespace APC.Applications.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICountryRepository _countryRepository;
        private readonly INationalityRepository _nationalityRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IProfessionRepository _professionRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMaritalStatusRepository _maritalStatusRepository;
        private readonly IEmploymentStatusRepository _employmentStatusRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IMembershipStatusRepository _membershipStatusRepository;
        private readonly INextOfKinRepository _nextOfKinRepository;
        private readonly IGeneralMeetingRepository _generalMeetingRepository;
        private readonly IGeneralMeetingAttendanceRepository _generalMeetingAttendanceRepository;
        private readonly IAttendanceStatusRepository _attendanceStatusRepository;

        public MemberService(IMemberRepository repository, ICurrentUserService currentUserService, ICountryRepository countryRepository,
            INationalityRepository nationalityRepository, IPositionRepository positionRepository, IProfessionRepository professionRepository,
            IPermissionRepository permissionRepository, IMaritalStatusRepository maritalStatusRepository, IEmploymentStatusRepository employmentStatusRepository,
            IGenderRepository genderRepository, IMembershipStatusRepository membershipStatusRepository, INextOfKinRepository nextOfKinRepository, 
            IGeneralMeetingRepository generalMeetingRepository, IGeneralMeetingAttendanceRepository generalMeetingAttendanceRepository,
            IAttendanceStatusRepository attendanceStatusRepository)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _countryRepository = countryRepository;
            _nationalityRepository = nationalityRepository;
            _positionRepository = positionRepository;
            _professionRepository = professionRepository;
            _permissionRepository = permissionRepository;
            _maritalStatusRepository = maritalStatusRepository;
            _genderRepository = genderRepository;
            _membershipStatusRepository = membershipStatusRepository;
            _employmentStatusRepository = employmentStatusRepository;
            _nextOfKinRepository = nextOfKinRepository;
            _generalMeetingRepository = generalMeetingRepository;
            _generalMeetingAttendanceRepository = generalMeetingAttendanceRepository;
            _attendanceStatusRepository = attendanceStatusRepository;
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

        public List<MemberFullDetailsDTO> GetAll()
        {
            string status = "Current";

            var data = (from m in _repository.GetAll()
                        join c in _countryRepository.GetAll() on m.countryID equals c.countryID
                        join n in _nationalityRepository.GetAll() on m.nationalityID equals n.nationalityID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join pro in _professionRepository.GetAll() on m.professionID equals pro.professionID
                        join per in _permissionRepository.GetAll() on m.permissionID equals per.permissionID
                        join mar in _maritalStatusRepository.GetAll() on m.maritalStatusID equals mar.maritalStatusID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join mb in _membershipStatusRepository.GetByStatus(status) on m.membershipStatusID equals mb.membershipStatusID
                        join e in _employmentStatusRepository.GetAll() on m.employmentStatusID equals e.employmentStatusID
                        join nk in _nextOfKinRepository.GetAll() on m.relationshipToKinID equals nk.NextOfKinId
                        select new
                        {
                            m.memberID,
                            m.name,
                            m.surname,
                            m.birthday,
                            m.imagePath,
                            m.houseAddress,
                            m.emailAddress,
                            m.membershipDate,
                            c.countryName,
                            n.nationality1,
                            pro.profession1,
                            p.positionName,
                            g.genderName,
                            e.employmentStatus,
                            mar.maritalStatus,
                            per.permission1,
                            m.phoneNumber,
                            m.phoneNumber2,
                            m.phoneNumber3,
                            mb.membershipStatus,
                            m.deadDate,
                            m.nextOfKin,
                            nk.NextOfKinName,
                            m.LGAOfCountryOrigin,
                        })
                        .ToList();

            return data.Select(x => new MemberFullDetailsDTO
            {
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                Birthday = x.birthday,
                ImagePath = x.imagePath,
                HouseAddress = x.houseAddress,
                Email = x.emailAddress,
                MembershipDate = x.membershipDate,
                Country = x.countryName,
                Nationality = x.nationality1,
                Profession = x.profession1,
                Position = x.positionName,
                Gender = x.genderName,
                EmploymentStatus = x.employmentStatus,
                MaritalStatus = x.maritalStatus,
                Permission = x.permission1,
                PhoneNumber = x.phoneNumber,
                PhoneNumber2 = x.phoneNumber2,
                PhoneNumber3 = x.phoneNumber3,
                MembershipStatus = x.membershipStatus,
                DeadDate = x.deadDate,
                NextOfKin = x.NextOfKinName,
                RelationshipToNextOfKin = x.nextOfKin,
                LGA = x.LGAOfCountryOrigin,
            })
            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            .ToList();
        }
        
        public List<MemberFullDetailsDTO> GetAllDeletedMembers()
        {
            var data = (from m in _repository.GetAllDeletedMembers()
                        join c in _countryRepository.GetAll() on m.countryID equals c.countryID
                        join n in _nationalityRepository.GetAll() on m.nationalityID equals n.nationalityID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join pro in _professionRepository.GetAll() on m.professionID equals pro.professionID
                        join per in _permissionRepository.GetAll() on m.permissionID equals per.permissionID
                        join mar in _maritalStatusRepository.GetAll() on m.maritalStatusID equals mar.maritalStatusID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join mb in _membershipStatusRepository.GetAll() on m.membershipStatusID equals mb.membershipStatusID
                        join e in _employmentStatusRepository.GetAll() on m.employmentStatusID equals e.employmentStatusID
                        join nk in _nextOfKinRepository.GetAll() on m.relationshipToKinID equals nk.NextOfKinId
                        select new
                        {
                            m.memberID,
                            m.name,
                            m.surname,
                            m.birthday,
                            m.imagePath,
                            m.houseAddress,
                            m.emailAddress,
                            m.membershipDate,
                            c.countryName,
                            n.nationality1,
                            pro.profession1,
                            p.positionName,
                            g.genderName,
                            e.employmentStatus,
                            mar.maritalStatus,
                            per.permission1,
                            m.phoneNumber,
                            m.phoneNumber2,
                            m.phoneNumber3,
                            mb.membershipStatus,
                            m.deadDate,
                            m.nextOfKin,
                            nk.NextOfKinName,
                            m.LGAOfCountryOrigin,
                        })
                        .ToList();

            return data.Select(x => new MemberFullDetailsDTO
            {
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                Birthday = x.birthday,
                ImagePath = x.imagePath,
                HouseAddress = x.houseAddress,
                Email = x.emailAddress,
                MembershipDate = x.membershipDate,
                Country = x.countryName,
                Nationality = x.nationality1,
                Profession = x.profession1,
                Position = x.positionName,
                Gender = x.genderName,
                EmploymentStatus = x.employmentStatus,
                MaritalStatus = x.maritalStatus,
                Permission = x.permission1,
                PhoneNumber = x.phoneNumber,
                PhoneNumber2 = x.phoneNumber2,
                PhoneNumber3 = x.phoneNumber3,
                MembershipStatus = x.membershipStatus,
                DeadDate = x.deadDate,
                NextOfKin = x.NextOfKinName,
                RelationshipToNextOfKin = x.nextOfKin,
                LGA = x.LGAOfCountryOrigin,
            })
            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            .ToList();
        }


        public List<BirthdayMembersDTO> GetBirthdayMembers(int month)
        {
            var data = (from m in _repository.GetAllBirthdayMembers(month)                       
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        select new
                        {
                            m.memberID,
                            m.name,
                            m.surname,
                            m.birthday,
                            m.imagePath,
                            p.positionName,
                            g.genderName,
                        })
                        .ToList();

            return data.Select(x => new BirthdayMembersDTO
            {
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                Birthday =  x.birthday.ToString("dd.MM.YYYY"),
                ImagePath = x.imagePath,
                Position = x.positionName,
                Gender = x.genderName,
            })
            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            .ToList();
        }

        public List<MembersBasicDetailDTO> GetInactiveMembers()
        {
            string status = "Inactive";

            var data = (from m in _repository.GetAll()
                        join n in _nationalityRepository.GetAll() on m.nationalityID equals n.nationalityID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join ms in _membershipStatusRepository.GetByStatus(status) on m.membershipStatusID equals ms.membershipStatusID
                        select new
                        {
                            m.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            n.nationality1,
                            p.positionName,
                            g.genderName,
                        })
                        .ToList();

            return data.Select(x => new MembersBasicDetailDTO
            {
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                Nationality = x.nationality1,
                Position = x.positionName,
                Gender = x.genderName,
            })
            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            .ToList();
        }

        public List<MembersBasicDetailDTO> GetFormerMembers()
        {
            string status = "Former";

            var data = (from m in _repository.GetAll()
                        join n in _nationalityRepository.GetAll() on m.nationalityID equals n.nationalityID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join ms in _membershipStatusRepository.GetByStatus(status) on m.membershipStatusID equals ms.membershipStatusID
                        select new
                        {
                            m.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            n.nationality1,
                            p.positionName,
                            g.genderName,
                        })
                        .ToList();

            return data.Select(x => new MembersBasicDetailDTO
            {
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                Nationality = x.nationality1,
                Position = x.positionName,
                Gender = x.genderName,
            })
            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            .ToList();
        }

        public List<DeadMemberShortDetailDTO> GetDeceasedMembers()
        {
            string status = "Deceased";

            var data = (from m in _repository.GetAll()
                        join n in _nationalityRepository.GetAll() on m.nationalityID equals n.nationalityID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join ms in _membershipStatusRepository.GetByStatus(status)
                            on m.membershipStatusID equals ms.membershipStatusID
                        select new
                        {
                            m.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            m.birthday,
                            m.deadDate,
                            p.positionName,
                            g.genderName,
                        })
                        .ToList();

            return data.Select(x =>
            {
                double age = 0;

                if (x.deadDate != null)
                {
                    var difference = x.deadDate - x.birthday;
                    age = Math.Floor(difference.TotalDays / 365.25);
                }

                return new DeadMemberShortDetailDTO
                {
                    MemberId = x.memberID,
                    FirstName = x.name,
                    LastName = x.surname,
                    ImagePath = x.imagePath,
                    Birthdate = x.birthday.ToString("dd.MM.yyyy"),
                    DeadDate = x.deadDate?.ToString("dd.MM.yyyy"),
                    Position = x.positionName,
                    Gender = x.genderName,
                    Age = age
                };
            })
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public int Get3MonthsAbsentesCount()
        {
            var last3Meetings = _generalMeetingRepository.GetAll()
                .OrderByDescending(x => x.year)
                .ThenByDescending(x => x.monthID)
                .Take(3)
                .Select(x => x.generalAttendanceID)
                .ToList();

            var absentStatusId = _attendanceStatusRepository.GetAll()
                .Where(x => x.attendanceStatus == "Absent")
                .Select(x => x.attendanceStatusID)
                .FirstOrDefault();

            var result = _generalMeetingAttendanceRepository.GetAll()
                .Where(a => last3Meetings.Contains(a.generalAttendanceID)
                            && a.attendanceStatusID == absentStatusId)
                .GroupBy(a => a.memberID)
                .Where(g => g.Count() == 3)
                .Count();

            return result;
        }

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
