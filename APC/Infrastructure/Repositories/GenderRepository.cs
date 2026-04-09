using APC.DAL;
using APC.Applications.Interfaces;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly APCEntities _db;
        public GenderRepository(APCEntities db)
        {
            _db = db;
        }

        public IQueryable<GENDER> GetAll()
        {
            return _db.GENDER;
        }

        public IQueryable<GENDER> GetById(int id)
        {
            return _db.GENDER.Where(x => x.genderID == id);
        }
    }
}
