using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace APC.Infrastructure.Repositories
{    
    public class ConstitutionRepository : IConstitutionRepository
    {
        private readonly APCEntities _db;
        public ConstitutionRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.CONSTITUTION.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.CONSTITUTION.First(x => x.constitutionID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string constitutionText, decimal fine)
        {
            return _db.CONSTITUTION.Any(x => !x.isDeleted && x.constitution1 == constitutionText && x.fine == fine);
        }

        public List<Constitution> GetAll()
        {
            var data = _db.CONSTITUTION
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.fine)
                .ThenBy(x => x.ShortDescription)
                .ToList();

            return data
                .Select(x => Constitution.Rehydrate(
                    x.constitutionID,
                    x.constitution1,
                    x.fine,
                    x.section,
                    x.ShortDescription
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.CONSTITUTION.First(x => x.constitutionID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public Constitution GetById(int id)
        {
            var entity = _db.CONSTITUTION
                .Where(x => x.constitutionID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.constitutionID,
                    x.constitution1,
                    x.fine,
                    x.section,
                    x.ShortDescription
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return Constitution.Rehydrate(
                entity.constitutionID,
                entity.constitution1,
                entity.fine,
                entity.section,
                entity.ShortDescription
            );
        }

        public bool Insert(Constitution data)
        {

            _db.CONSTITUTION.Add(new CONSTITUTION
            {
                constitution1 = data.ConstitutionText,
                fine = data.Fine,
                section = data.Section,
                ShortDescription = data.ShortDescription,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.CONSTITUTION.FirstOrDefault(x => x.constitutionID == id);

            if (entity == null)
                return false;

            _db.CONSTITUTION.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Constitution data)
        {
            var entity = _db.CONSTITUTION.First(x => x.constitutionID == data.ConstitutionId);
            entity.constitution1 = data.ConstitutionText;
            entity.fine = data.Fine;
            entity.section = data.Section;
            entity.ShortDescription = data.ShortDescription;

            _db.SaveChanges();
            return true;
        }
    }
}
