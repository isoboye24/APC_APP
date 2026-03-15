using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

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

        public List<SpecialContributor> GetAll()
        {
            var data = _db.SPECIAL_CONTRIBUTORS
                .Where(x => !x.isDeleted)
                .ToList();

            return data
                .Select(x => SpecialContributor.Rehydrate(
                    x.specialContributorID,
                    x.memberID,
                    x.amountContributed,
                    x.contributedDate,
                    x.summary,
                    x.specialContributionID
                ))
                .ToList();
        }

        public List<SpecialContributor> GetAllDeleted()
        {
            var data = _db.SPECIAL_CONTRIBUTORS
                .Where(x => x.isDeleted)
                .ToList();

            return data
                .Select(x => SpecialContributor.Rehydrate(
                    x.specialContributorID,
                    x.memberID,
                    x.amountContributed,
                    x.contributedDate,
                    x.summary,
                    x.specialContributionID
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTORS.First(x => x.specialContributorID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public SpecialContributor GetById(int id)
        {
            var entity = _db.SPECIAL_CONTRIBUTORS
                .Where(x => x.specialContributorID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.specialContributorID,
                    x.memberID,
                    x.amountContributed,
                    x.contributedDate,
                    x.summary,
                    x.specialContributionID
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return SpecialContributor.Rehydrate(
                    entity.specialContributorID,
                    entity.memberID,
                    entity.amountContributed,
                    entity.contributedDate,
                    entity.summary,
                    entity.specialContributionID
            );
        }

        public List<SpecialContributorFullDetails> GetFullSpecialContributorDetails()
        {
            var contribution = (from sc in _db.SPECIAL_CONTRIBUTORS.Where(x => !x.isDeleted)
                                join m in _db.MEMBER on sc.memberID equals m.memberID
                                join scn in _db.SPECIAL_CONTRIBUTIONS on sc.specialContributionID equals scn.specialContributionID
                                select new SpecialContributorFullDetails
                                {
                                    SpecialContributorId = sc.specialContributorID,
                                    MemberId = sc.memberID,
                                    FirstName = m.name,
                                    LastName = m.surname,
                                    ImagePath = m.imagePath,
                                    AmountContributed = sc.amountContributed,
                                    AmountExpected = scn.amountExpected,
                                    Balance = scn.amountExpected - sc.amountContributed,
                                    ContributedDate = sc.contributedDate,
                                    PaymentStatus = sc.amountContributed == scn.amountExpected ? "Completed" : sc.amountContributed < scn.amountExpected ? "Incomplete" : "Extra " + (sc.amountContributed - scn.amountExpected).ToString(),
                                    Summary = sc.summary,
                                    SpecialContributionId = sc.specialContributionID,
                                });

            return contribution.ToList();
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
