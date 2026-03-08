using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Infrastructure.Repositories
{
    public class FinedMemberRepository : IFinedMemberRepository
    {
        private readonly APCEntities _db;
        public FinedMemberRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.FINED_MEMBER.Count(x => !x.isdeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.FINED_MEMBER.First(x => x.finedMemberID == id);
            entity.isdeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(int constitutionId, int memberId, DateTime fineDate)
        {
            return _db.FINED_MEMBER.Any(x => !x.isdeleted && x.constitutionID == constitutionId && x.memberID == memberId 
                                        && x.fineDate.Day == fineDate.Day && x.fineDate.Month == fineDate.Month
                                        && x.fineDate.Year == fineDate.Year);
        }

        public List<FinedMember> GetAll()
        {
            var data = _db.FINED_MEMBER
                .Where(x => !x.isdeleted)
                .ToList();

            return data
                .Select(x => FinedMember.Rehydrate(
                    x.finedMemberID,
                    x.amountPaid,
                    x.summary,
                    x.constitutionID,
                    x.memberID,
                    x.fineDate
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.FINED_MEMBER.First(x => x.finedMemberID == id);
            entity.isdeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public FinedMember GetById(int id)
        {
            var entity = _db.FINED_MEMBER
                .Where(x => x.finedMemberID == id && !x.isdeleted)
                .Select(x => new
                {
                    x.finedMemberID,
                    x.amountPaid,
                    x.summary,
                    x.constitutionID,
                    x.memberID,
                    x.fineDate
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return FinedMember.Rehydrate(
                    entity.finedMemberID,
                    entity.amountPaid,
                    entity.summary,
                    entity.constitutionID,
                    entity.memberID,
                    entity.fineDate
            );
        }

        public bool Insert(FinedMember data)
        {
            _db.FINED_MEMBER.Add(new FINED_MEMBER
            {
                finedMemberID = data.FinedMemberId,
                amountPaid = data.AmountPaid,
                summary = data.Summary,
                constitutionID = data.ConstitutionId,
                memberID = data.MemberId,
                fineDate = data.FineDate,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.FINED_MEMBER.FirstOrDefault(x => x.finedMemberID == id);

            if (entity == null)
                return false;

            _db.FINED_MEMBER.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(FinedMember data)
        {
            var entity = _db.FINED_MEMBER.First(x => x.finedMemberID == data.FinedMemberId);
            entity.amountPaid = data.AmountPaid;
            entity.summary = data.Summary;
            entity.constitutionID = data.ConstitutionId;
            entity.memberID = data.MemberId;
            entity.fineDate = data.FineDate;

            _db.SaveChanges();
            return true;
        }
    }
}
