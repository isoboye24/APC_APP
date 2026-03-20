using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
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

        public Gender GetById(int id)
        {
            var entity = _db.GENDER.FirstOrDefault(x => x.genderID == id);
            if (entity == null) return null;

            var gender = new Gender(entity.genderName);
            gender.SetId(entity.genderID);
            return gender;
        }
    }
}
