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
    public class NextOfKinRepository : INextOfKinRepository
    {
        private readonly APCEntities _db;
        public NextOfKinRepository(APCEntities db)
        {
            _db = db;
        }

        public List<NextOfKin> GetAll()
        {
            var data = _db.NEXT_OF_KIN_RELATIONSHIP
                .OrderBy(x => x.RelationshipToKinID)
                .ToList();

            return data
                .Select(x => NextOfKin.Rehydrate(
                    x.RelationshipToKinID,
                    x.RelationshipToKin
                ))
                .ToList();
        }

        public NextOfKin GetById(int id)
        {
            var entity = _db.NEXT_OF_KIN_RELATIONSHIP.FirstOrDefault(x => x.RelationshipToKinID == id);
            if (entity == null) return null;

            var nextOfKinRel = new NextOfKin(entity.RelationshipToKin);
            nextOfKinRel.SetId(entity.RelationshipToKinID);
            return nextOfKinRel;
        }
    }
}
