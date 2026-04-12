using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Infrastructure.Repositories
{
    public class MembershipStatusRepository : IMembershipStatusRepository
    {
        private readonly APCEntities _db;
        public MembershipStatusRepository(APCEntities db)
        {
            _db = db;
        }

        public IQueryable<MEMBERSHIP_STATUS> GetAll()
        {
            return _db.MEMBERSHIP_STATUS;
        }

        public IQueryable<MEMBERSHIP_STATUS> GetByStatus(string status)
        {
            return _db.MEMBERSHIP_STATUS.Where(x => x.membershipStatus == status);
        }

        public MembershipStatus GetById(int id)
        {
            var entity = _db.MEMBERSHIP_STATUS.FirstOrDefault(x => x.membershipStatusID == id);
            if (entity == null) return null;

            var status = new MembershipStatus(entity.membershipStatus);
            status.SetId(entity.membershipStatusID);
            return status;
        }

    }
}
