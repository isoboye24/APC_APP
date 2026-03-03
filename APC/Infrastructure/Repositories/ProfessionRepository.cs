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
    public class ProfessionRepository : IProfessionRepository
    {
        private readonly APCEntities _db;
        public ProfessionRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.PROFESSION.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.PROFESSION.First(x => x.professionID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string name)
        {
            return _db.PROFESSION.Any(x => !x.isDeleted && x.profession1 == name);
        }

        public List<Profession> GetAll()
        {
            var data = _db.PROFESSION
                .Where(x => !x.isDeleted)
                .OrderBy(x => x.profession1)
                .ToList();

            return data
                .Select(x => Profession.Rehydrate(
                    x.professionID,
                    x.profession1
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.PROFESSION.First(x => x.professionID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public Profession GetById(int id)
        {
            var entity = _db.PROFESSION.FirstOrDefault(x => x.professionID == id && !x.isDeleted);
            if (entity == null) return null;

            var profession = new Profession(entity.profession1);
            profession.SetId(entity.professionID);
            return profession;
        }

        public bool Insert(Profession profession)
        {
            _db.PROFESSION.Add(new PROFESSION
            {
                profession1 = profession.ProfessionName
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.PROFESSION.FirstOrDefault(x => x.professionID == id);

            if (entity == null)
                return false;

            _db.PROFESSION.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Profession profession)
        {
            var entity = _db.PROFESSION.First(x => x.professionID == profession.ProfessionId);
            entity.profession1 = profession.ProfessionName;
            _db.SaveChanges();
            return true;
        }
    }
}
