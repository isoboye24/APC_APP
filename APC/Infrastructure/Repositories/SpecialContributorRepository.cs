using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class SpecialContributorRepository : ISpecialContributorRepository
    {
        private readonly APCEntities _db; 
        public SpecialContributorRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.SPECIAL_CONTRIBUTORS.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTORS.First(x => x.specialContributorID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(int memberId, int specialContributionId)
        {
            return _db.SPECIAL_CONTRIBUTORS.Any(x => !x.isDeleted && x.memberID == memberId && x.specialContributionID == specialContributionId);
        }

        public IQueryable<SPECIAL_CONTRIBUTORS> GetAll()
        {
            return _db.SPECIAL_CONTRIBUTORS.Where(x => !x.isDeleted);
        }
        
        public IQueryable<SPECIAL_CONTRIBUTORS> GetAllDeletedContributors()
        {
            return _db.SPECIAL_CONTRIBUTORS.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTORS.First(x => x.specialContributorID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<SPECIAL_CONTRIBUTORS> GetById(int id)
        {
            return _db.SPECIAL_CONTRIBUTORS.Where(x => !x.isDeleted && x.specialContributorID == id);
        }
        
        public decimal GetByContributionId(int id)
        {
            decimal totalAmount = _db.SPECIAL_CONTRIBUTORS.Where(x => !x.isDeleted && x.specialContributionID == id).Sum(x => x.amountContributed);
            return totalAmount;
        }

        public bool Insert(SpecialContributor data)
        {
            _db.SPECIAL_CONTRIBUTORS.Add(new SPECIAL_CONTRIBUTORS
            {
                memberID = data.MemberId,
                amountContributed = data.AmountContributed,
                contributedDate = data.ContributedDate,
                summary = data.Summary,
                specialContributionID = data.SpecialContributionId
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTORS.FirstOrDefault(x => x.specialContributorID == id);

            if (entity == null)
                return false;

            _db.SPECIAL_CONTRIBUTORS.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(SpecialContributor data)
        {
            var entity = _db.SPECIAL_CONTRIBUTORS.First(x => x.specialContributorID == data.SpecialContributorId);

            entity.memberID = data.MemberId;
            entity.amountContributed = data.AmountContributed;
            entity.contributedDate = data.ContributedDate;
            entity.summary = data.Summary;
            entity.specialContributionID = data.SpecialContributionId;

            _db.SaveChanges();
            return true;
        }
    }
}
