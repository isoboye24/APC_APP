using APC.Applications.Interfaces;
using APC.DAL;
using APC.Domain.Entities;
using System;
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

        public IQueryable<MEMBER> GetAll()
        {
            return _db.MEMBER.Where(x => !x.isDeleted);
        }

        public IQueryable<MEMBER> GetAllBirthdayMembers(int month)
        {
            return _db.MEMBER.Where(x => !x.isDeleted && x.birthday.Month == month);
        }
        
        public IQueryable<MEMBER> GetAllDeletedMembers()
        {
            return _db.MEMBER.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.MEMBER.First(x => x.memberID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public MEMBER GetById(int id)
        {
            return _db.MEMBER.FirstOrDefault(x => !x.isDeleted && x.memberID == id);
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
    }
}
