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
    public class MembershipStatusRepository : IMembershipStatusRepository
    {
        private readonly APCEntities _db;
        public MembershipStatusRepository(APCEntities db)
        {
            _db = db;
        }

        public List<MembershipStatus> GetAll()
        {
            var data = _db.MEMBERSHIP_STATUS
                .OrderBy(x => x.membershipStatusID)
                .ToList();

            return data
                .Select(x => MembershipStatus.Rehydrate(
                    x.membershipStatusID,
                    x.membershipStatus
                ))
                .ToList();
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
