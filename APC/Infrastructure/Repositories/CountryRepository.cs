using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly APCEntities _db;
        public CountryRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.COUNTRY.Count(x => !x.isDeleted);
        }

        public bool GetBack(int ID)
        {
            var entity = _db.COUNTRY.First(x => x.countryID == ID);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var entity = _db.COUNTRY.First(x => x.countryID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string name)
        {
            return _db.COUNTRY.Any(x => !x.isDeleted && x.countryName == name);
        }

        public IQueryable<COUNTRY> GetAll()
        {
            return _db.COUNTRY.Where(x => !x.isDeleted);
        }

        public IQueryable<COUNTRY> GetAllDeletedCountries()
        {
            return _db.COUNTRY.Where(x => x.isDeleted);
        }

        public Country GetById(int id)
        {
            var entity = _db.COUNTRY.FirstOrDefault(x => x.countryID == id && !x.isDeleted);
            if (entity == null) return null;

            var country = new Country(entity.countryName);
            country.SetId(entity.countryID);
            return country;
        }

        public bool Insert(Country country)
        {
            _db.COUNTRY.Add(new COUNTRY
            {
                countryName = country.CountryName
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.COUNTRY.FirstOrDefault(x => x.countryID == id);

            if (entity == null)
                return false;

            _db.COUNTRY.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Country country)
        {
            var entity = _db.COUNTRY.First(x => x.countryID == country.CountryId);
            entity.countryName = country.CountryName;
            _db.SaveChanges();
            return true;
        }
    }
}
