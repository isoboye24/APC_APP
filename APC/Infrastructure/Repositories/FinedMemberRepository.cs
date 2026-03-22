using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

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

        public IQueryable<FINED_MEMBER> GetAll()
        {
            return _db.FINED_MEMBER.Where(x => !x.isdeleted);
        }

        public IQueryable<FINED_MEMBER> GetAllDeletedFinedMembers()
        {
            return _db.FINED_MEMBER.Where(x => x.isdeleted);
        }

        

        public bool GetBack(int id)
        {
            var entity = _db.FINED_MEMBER.First(x => x.finedMemberID == id);
            entity.isdeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<FINED_MEMBER> GetById(int id)
        {
            return _db.FINED_MEMBER.Where(x => !x.isdeleted && x.finedMemberID == id);
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
