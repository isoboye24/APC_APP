using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class EmploymentStatusRepository : IEmploymentStatusRepository
    {
        private readonly APCEntities _db;
        public EmploymentStatusRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.EMPLOYMENT_STATUS.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.EMPLOYMENT_STATUS.First(x => x.employmentStatusID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string name)
        {
            return _db.EMPLOYMENT_STATUS.Any(x => !x.isDeleted && x.employmentStatus == name);
        }

        public IQueryable<EMPLOYMENT_STATUS> GetAll()
        {
            return _db.EMPLOYMENT_STATUS.Where(x => !x.isDeleted);
        }
        
        public IQueryable<EMPLOYMENT_STATUS> GetAllEmploymentStatuses()
        {
            return _db.EMPLOYMENT_STATUS.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.EMPLOYMENT_STATUS.First(x => x.employmentStatusID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<EMPLOYMENT_STATUS> GetById(int id)
        {
            return _db.EMPLOYMENT_STATUS.Where(x => !x.isDeleted && x.employmentStatusID == id);
        }

        public bool Insert(EmploymentStatus status)
        {
            _db.EMPLOYMENT_STATUS.Add(new EMPLOYMENT_STATUS
            {
                employmentStatus = status.EmploymentStatusName
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.EMPLOYMENT_STATUS.FirstOrDefault(x => x.employmentStatusID == id);

            if (entity == null)
                return false;

            _db.EMPLOYMENT_STATUS.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(EmploymentStatus status)
        {
            var entity = _db.EMPLOYMENT_STATUS.First(x => x.employmentStatusID == status.EmploymentStatusId);
            entity.employmentStatus = status.EmploymentStatusName;
            _db.SaveChanges();
            return true;
        }
    }
}
