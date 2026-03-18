using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class NationalityRepository : INationalityRepository
    {
        private readonly APCEntities _db;
        public NationalityRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.NATIONALITY.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.NATIONALITY.First(x => x.nationalityID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string name)
        {
            return _db.NATIONALITY.Any(x => !x.isDeleted && x.nationality1 == name);
        }

        public IQueryable<NATIONALITY> GetAll()
        {
            return _db.NATIONALITY.Where(x => !x.isDeleted);
        }

        public IQueryable<NATIONALITY> GetAllDeletedNationalities()
        {
            return _db.NATIONALITY.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.NATIONALITY.First(x => x.nationalityID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public Nationality GetById(int id)
        {
            var entity = _db.NATIONALITY.FirstOrDefault(x => x.nationalityID == id && !x.isDeleted);
            if (entity == null) return null;

            var nationality = new Nationality(entity.nationality1);
            nationality.SetId(entity.nationalityID);
            return nationality;
        }

        public bool Insert(Nationality nationality)
        {
            _db.NATIONALITY.Add(new NATIONALITY
            {
                nationality1 = nationality.NationalityName
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.NATIONALITY.FirstOrDefault(x => x.nationalityID == id);

            if (entity == null)
                return false;

            _db.NATIONALITY.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Nationality nationality)
        {
            var entity = _db.NATIONALITY.First(x => x.nationalityID == nationality.NationalityId);
            entity.nationality1 = nationality.NationalityName;
            _db.SaveChanges();
            return true;
        }
    }
}
