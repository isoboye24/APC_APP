using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class SpecialContributionRepository : ISpecialContributionRepository
    {
        private readonly APCEntities _db;

        public SpecialContributionRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.SPECIAL_CONTRIBUTIONS.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTIONS.First(x => x.specialContributionID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string title)
        {
            return _db.SPECIAL_CONTRIBUTIONS.Any(x => !x.isDeleted && x.title == title);
        }

        public IQueryable<SPECIAL_CONTRIBUTIONS> GetAll()
        {
            return _db.SPECIAL_CONTRIBUTIONS.Where(x => !x.isDeleted);
        }
        
        public IQueryable<SPECIAL_CONTRIBUTIONS> GetAllDeletedSpecialContributions()
        {
            return _db.SPECIAL_CONTRIBUTIONS.Where(x => x.isDeleted);
        }

        
        public bool GetBack(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTIONS.First(x => x.specialContributionID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<SPECIAL_CONTRIBUTIONS> GetById(int id)
        {
            return _db.SPECIAL_CONTRIBUTIONS.Where(x => !x.isDeleted && x.specialContributionID == id);
        }

        public bool Insert(SpecialContribution data)
        {
            _db.SPECIAL_CONTRIBUTIONS.Add(new SPECIAL_CONTRIBUTIONS
            {
                title = data.Title,
                summary = data.Summary,
                amountToContribute = data.AmountToContribute,
                supervisorID = data.SupervisorId,
                contributionStartDate = data.ContributionStartDate,
                contributionEndDate = data.ContributionEndDate,
                amountExpected = data.AmountExpected,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTIONS.FirstOrDefault(x => x.specialContributionID == id);

            if (entity == null)
                return false;

            _db.SPECIAL_CONTRIBUTIONS.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(SpecialContribution data)
        {
            var entity = _db.SPECIAL_CONTRIBUTIONS.First(x => x.specialContributionID == data.SpecialContributionId);

            entity.title = data.Title;
            entity.summary = data.Summary;
            entity.amountToContribute = data.AmountToContribute;
            entity.supervisorID = data.SupervisorId;
            entity.contributionStartDate = data.ContributionStartDate;
            entity.contributionEndDate = data.ContributionEndDate;
            entity.amountExpected = data.AmountExpected;

            _db.SaveChanges();
            return true;
        }
    }
}
