using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using APC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            => _repository.GetAll();

        public List<MembersBasicDetailDTO> GetAllDeletedMembers()
            => _repository.GetAllDeletedMembers();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public List<BirthdayMembersDTO> GetBirthdayMembers(int month)
            => _repository.GetBirthdayMembers(month);

        public List<DeadMemberShortDetailDTO> GetDeceasedMembers()
            => _repository.GetDeceasedMembers();

        public List<MembersBasicDetailDTO> GetFormerMembers()
            => _repository.GetFormerMembers();

        public List<MemberFullDetailsDTO> GetFullMemberDetails()
            => _repository.GetFullMemberDetails();

        public List<MembersBasicDetailDTO> GetInactiveMembers()
            => _repository.GetInactiveMembers();

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
    }
}
