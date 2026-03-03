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
    public class GenderRepository : IGenderRepository
    {
        private readonly APCEntities _db;
        public GenderRepository(APCEntities db)
        {
            _db = db;
        }

        public List<Gender> GetAll()
        {
            var data = _db.GENDER
                .OrderBy(x => x.genderID)
                .ToList();

            return data
                .Select(x => Gender.Rehydrate(
                    x.genderID,
                    x.genderName
                ))
                .ToList();
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
