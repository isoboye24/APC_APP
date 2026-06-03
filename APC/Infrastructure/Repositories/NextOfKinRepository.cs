using APC.Infrastructure.Data;
using APC.Applications.Interfaces;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class NextOfKinRepository : INextOfKinRepository
    {
        private readonly APCEntities _db;
        public NextOfKinRepository(APCEntities db)
        {
            _db = db;
        }

        public IQueryable<NEXT_OF_KIN_RELATIONSHIP> GetAll()
        {
            return _db.NEXT_OF_KIN_RELATIONSHIP;
        }

        public IQueryable<NEXT_OF_KIN_RELATIONSHIP> GetById(int Id)
        {
            return _db.NEXT_OF_KIN_RELATIONSHIP.Where(x => x.RelationshipToKinID == Id);
        }
    }
}
