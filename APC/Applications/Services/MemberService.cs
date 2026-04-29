using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.DAL.DTO;
using APC.Domain.Entities;
using APC.Helper;
using APC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

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

        public bool DeleteMemberPermission(int id)
            => _repository.DeleteMemberPermission(id);

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
        
        public List<MemberFullDetailsDTO> SelectSpecificMember(int id)
        {
            string status = "Current";

            var data = (from m in _repository.GetAll().Where(x => x.memberID == id)
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
                    DeadDate = x.deadDate.ToString("dd.MM.yyyy"),
                    Position = x.positionName,
                    Gender = x.genderName,
                    Age = age.ToString()
                };
            })
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ToList();
        }

        public List<MembersBasicDetailDTO> GetAllMembersWithAccess()
        {
            string status = "Current";
            string permissionStatus = "Member";

            var data = (from m in _repository.GetAll()
                        join per in _permissionRepository.GetAllStatusExcept(permissionStatus) on m.permissionID equals per.permissionID
                        join p in _positionRepository.GetAll() on m.positionID equals p.positionID
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join ms in _membershipStatusRepository.GetByStatus(status) on m.membershipStatusID equals ms.membershipStatusID
                        select new
                        {
                            m.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            per.permission1,
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
                Permission = x.permission1,
                Position = x.positionName,
                Gender = x.genderName,
            })
            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            .ToList();
        }

        public List<MembersBasicDetailDTO> Select3MonthsAbsentes()
        {
            string attendanceStatus = "Absent";

            var recentGeneralMeetings = _generalMeetingRepository.GetAll()
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .ThenByDescending(x => x.monthID)
                .Take(3)
                .Select(x => x.generalAttendanceID)
                .ToList();

            var absentMemberIds = (from p in _generalMeetingAttendanceRepository.GetAll()
                                   join ats in _attendanceStatusRepository.GetByStatus(attendanceStatus)
                                       on p.attendanceStatusID equals ats.attendanceStatusID
                                   where !p.isDeleted && recentGeneralMeetings.Contains(p.generalAttendanceID)
                                   select p.memberID)
                                   .ToList();

            var membersAppearingThreeTimes = GeneralHelper.FindMembersAppearingThreeTimes(absentMemberIds);

            var data = (from m in _repository.GetAll()
                        join g in _genderRepository.GetAll() on m.genderID equals g.genderID
                        join pos in _positionRepository.GetAll() on m.positionID equals pos.positionID
                        join ms in _membershipStatusRepository.GetByStatus("Current")
                            on m.membershipStatusID equals ms.membershipStatusID
                        where !m.isDeleted && membersAppearingThreeTimes.Contains(m.memberID)
                        select new
                        {
                            m.memberID,
                            m.name,
                            m.surname,
                            m.imagePath,
                            m.positionID,
                            pos.positionName,
                            m.genderID,
                            g.genderName
                        })
                        .ToList();

            return data.Select(x => new MembersBasicDetailDTO
            {
                MemberId = x.memberID,
                FirstName = x.name,
                LastName = x.surname,
                ImagePath = x.imagePath,
                Position = x.positionName,
                Gender = x.genderName,
            })
            .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
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

            data.MemberAuthentication.UpdateUsername(data.MemberAuthentication.Username);
            data.MemberAuthentication.UpdatePasswordHash(data.MemberAuthentication.PasswordHash);

            data.PersonalInfo.UpdateFirstName(data.PersonalInfo.FirstName);
            data.PersonalInfo.UpdateLastName(data.PersonalInfo.LastName);
            data.PersonalInfo.UpdateBirthday(data.PersonalInfo.Birthday);
            data.PersonalInfo.UpdateImagePath(data.PersonalInfo.ImagePath);
            data.PersonalInfo.UpdateGender(data.PersonalInfo.GenderId);

            data.ContactInfo.UpdateEmail(data.ContactInfo.Email);
            data.ContactInfo.UpdateHouseAddress(data.ContactInfo.HouseAddress);
            data.ContactInfo.UpdatePhoneNumber(data.ContactInfo.PhoneNumber);
            data.ContactInfo.UpdatePhoneNumber2(data.ContactInfo.PhoneNumber2);
            data.ContactInfo.UpdatePhoneNumber3(data.ContactInfo.PhoneNumber3);

            data.MembershipInfo.UpdateMembershipDate((DateTime)data.MembershipInfo.MembershipDate);
            data.MembershipInfo.UpdateMembershipStatus(data.MembershipInfo.MembershipStatusId);
            data.MembershipInfo.UpdatePosition(data.MembershipInfo.PositionId);
            data.MembershipInfo.UpdatePermission(data.MembershipInfo.PermissionId);

            data.DemographicInfo.UpdateCountry(data.DemographicInfo.CountryId);
            data.DemographicInfo.UpdateNationality(data.DemographicInfo.NationalityId);
            data.DemographicInfo.UpdateProfession(data.DemographicInfo.ProfessionId);
            data.DemographicInfo.UpdateEmplomentStatus(data.DemographicInfo.EmploymentStatusId);
            data.DemographicInfo.UpdateMaritalStatus(data.DemographicInfo.MaritalStatusId);
            data.DemographicInfo.UpdateLGA(data.DemographicInfo.LGA);

            data.EmergencyContact.UpdateNextOfKin(data.EmergencyContact.NextOfKin);
            data.EmergencyContact.UpdateRelationshipToNextOfKin(data.EmergencyContact.RelationshipToNextOfKinId);

            data.LifeStatus.UpdateDeadDate(data.LifeStatus.DeadDate);

            return _repository.Update(data);
        }

        public int GetUniqueProfessionCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 1)
            .Select(m => m.professionID)
            .Distinct()
            .Count();

        }

        public int GetUniquePositionCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 1)
            .Select(m => m.positionID)
            .Distinct()
            .Count();

        }

        public int GetUniqueNationalityCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 1)
            .Select(m => m.nationalityID)
            .Distinct()
            .Count();

        }

        public int GetUniquePermissionCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 1)
            .Select(m => m.permissionID)
            .Distinct()
            .Count();

        }

        public int GetCurrentMaleCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 1)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Male");
        }
        
        public int GetCurrentFemaleCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 1)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Female");
        }
        
        public int GetCurrentDivisorCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 1)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Divisor");
        }

        public int GetFormerMaleCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 2)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Male");
        }
        
        public int GetFormerFemaleCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 2)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Female");
        }
        
        public int GetFormerDivisorCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 2)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Divisor");
        }

        public int GetDeceasedMaleCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 3)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Male");
        }

        public int GetDeceasedFemaleCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 3)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Female");
        }

        public int GetDeceasedDivisorCount()
        {
            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 3)
            .Join(_genderRepository.GetAll(),
              m => m.genderID,
              g => g.genderID,
              (m, g) => g.genderName)
            .Count(g => g == "Divisor");
        }

        public int SelectPermittedMembersCount()
        {
            string status = "Member";

            return _repository.GetAll()
            .Where(m => m.membershipStatusID == 1)
            .Join(_permissionRepository.GetAllStatusExcept(status),
              m => m.permissionID,
              p => p.permissionID,
              (m, p) => p.permission1)
            .Count();
        }
        
        public int GetPresentMembersCount(int ID)
        {
            string status = "Present";

            return _repository.GetAll()
                .Where(m => m.membershipStatusID == 1)
                .Join(_attendanceStatusRepository.GetByStatus(status),
                    m => m.memberID,
                    a => a.attendanceStatusID,
                    (m, a) => m)
                .Count();
        }

        public int GetAbsentMembersCount(int ID)
        {
            string status = "Absent";

            return _repository.GetAll()
                .Where(m => m.membershipStatusID == 1)
                .Join(_attendanceStatusRepository.GetByStatus(status),
                    m => m.memberID,
                    a => a.attendanceStatusID,
                    (m, a) => m)
                .Count();
        }

        public decimal GetAmountContributed(int ID)
        {
            return _generalMeetingAttendanceRepository.GetAll()
                   .Where(x => x.memberID == ID &&
                               !x.isDeleted &&
                               x.attendanceStatusID == 2 &&
                               x.monthlyDues > 0)
                   .Sum(x => (decimal?)x.monthlyDues) ?? 0;
                    
        }

        public decimal GetAmountExpected(int ID)
        {
            var memberInfo = _repository.GetById(ID);

            if (memberInfo == null || memberInfo.membershipDate == null)
                return 0;

            DateTime membershipDate = memberInfo.membershipDate.Value;

            int meetingCount = (from a in _generalMeetingAttendanceRepository.GetAll()
                                join g in _generalMeetingRepository.GetAll()
                                    on a.generalAttendanceID equals g.generalAttendanceID
                                where a.memberID == ID &&
                                      !g.isDeleted &&
                                      g.attendanceDate > membershipDate
                                select a)
                                .Count();

            decimal feePerMeeting = 10.0m;

            return meetingCount * feePerMeeting;
        }

    }
}

    
