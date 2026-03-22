using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

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

        public IQueryable<CONSTITUTION> GetAll()
        {
            return _db.CONSTITUTION.Where(x => !x.isDeleted);
        }
        
        public IQueryable<CONSTITUTION> GetAllDeletedConstitutions()
        {
            return _db.CONSTITUTION.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.CONSTITUTION.First(x => x.constitutionID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<CONSTITUTION> GetById(int id)
        {
            return _db.CONSTITUTION.Where(x => x.isDeleted && x.constitutionID == id);
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
