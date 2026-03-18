using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class MaritalStatusRepository : IMaritalStatusRepository
    {
        private readonly APCEntities _db;
        public MaritalStatusRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.MARITAL_STATUS.Count(x=> !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.MARITAL_STATUS.First(x => x.maritalStatusID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string name)
        {
            return _db.MARITAL_STATUS.Any(x => !x.isDeleted && x.maritalStatus == name);
        }

        public IQueryable<MARITAL_STATUS> GetAll()
        {
            return _db.MARITAL_STATUS.Where(x => !x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.MARITAL_STATUS.First(x => x.maritalStatusID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public MaritalStatus GetById(int id)
        {
            var entity = _db.MARITAL_STATUS.FirstOrDefault(x => x.maritalStatusID == id && !x.isDeleted);
            if (entity == null) return null;

            var maritalStatus = new MaritalStatus(entity.maritalStatus);
            maritalStatus.SetId(entity.maritalStatusID);
            return maritalStatus;
        }

        public bool Insert(MaritalStatus status)
        {
            _db.MARITAL_STATUS.Add(new MARITAL_STATUS
            {
                maritalStatus = status.MaritalStatusName
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.MARITAL_STATUS.FirstOrDefault(x => x.maritalStatusID == id);

            if (entity == null)
                return false;

            _db.MARITAL_STATUS.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(MaritalStatus status)
        {
            var entity = _db.MARITAL_STATUS.First(x => x.maritalStatusID == status.MaritalStatusId);
            entity.maritalStatus = status.MaritalStatusName;
            _db.SaveChanges();
            return true;
        }
    }
}
