using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

        public List<SpecialContribution> GetAll()
        {
            var data = _db.SPECIAL_CONTRIBUTIONS
                .Where(x => !x.isDeleted)
                .ToList();

            return data
                .Select(x => SpecialContribution.Rehydrate(
                    x.specialContributionID,
                    x.title,
                    x.summary,
                    x.amountToContribute,
                    x.supervisorID,
                    x.contributionStartDate,
                    x.contributionEndDate,
                    x.amountExpected
                ))
                .ToList();
        }

        public List<SpecialContribution> GetAllDeleted()
        {
            var data = _db.SPECIAL_CONTRIBUTIONS
                .Where(x => x.isDeleted)
                .ToList();

            return data
                .Select(x => SpecialContribution.Rehydrate(
                    x.specialContributionID,
                    x.title,
                    x.summary,
                    x.amountToContribute,
                    x.supervisorID,
                    x.contributionStartDate,
                    x.contributionEndDate,
                    x.amountExpected
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTIONS.First(x => x.specialContributionID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public SpecialContribution GetById(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTIONS
                .Where(x => x.specialContributionID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.specialContributionID,
                    x.title,
                    x.summary,
                    x.amountToContribute,
                    x.supervisorID,
                    x.contributionStartDate,
                    x.contributionEndDate,
                    x.amountExpected
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return SpecialContribution.Rehydrate(
                    entity.specialContributionID,
                    entity.title,
                    entity.summary,
                    entity.amountToContribute,
                    entity.supervisorID,
                    entity.contributionStartDate,
                    entity.contributionEndDate,
                    entity.amountExpected
            );
        }

        public List<SpecialContributionFullDetails> GetFullSpecialContributionDetails()
        {
            var contribution = (from sc in _db.SPECIAL_CONTRIBUTIONS.Where(x => !x.isDeleted)
                          join m in _db.MEMBER on sc.supervisorID equals m.memberID
                          select new SpecialContributionFullDetails
                          {
                              SpecialContributionId = sc.specialContributionID,
                              Title = sc.title,
                              Summary = sc.summary,
                              AmountToContribute = sc.amountToContribute,
                              SupervisorId = sc.supervisorID,
                              FirstName = m.name,
                              LastName = m.surname,
                              ImagePath = m.imagePath,
                              ContributionStartDate = sc.contributionStartDate,
                              ContributionEndDate = sc.contributionEndDate,
                              AmountExpected = sc.amountExpected
                          });

            return contribution.ToList();
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
