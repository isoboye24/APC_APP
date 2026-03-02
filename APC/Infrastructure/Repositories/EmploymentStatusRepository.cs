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

        public List<EmploymentStatus> GetAll()
        {
            var data = _db.EMPLOYMENT_STATUS
                .Where(x => !x.isDeleted)
                .OrderBy(x => x.employmentStatus)
                .ToList();

            return data
                .Select(x => EmploymentStatus.Rehydrate(
                    x.employmentStatusID,
                    x.employmentStatus
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.EMPLOYMENT_STATUS.First(x => x.employmentStatusID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public EmploymentStatus GetById(int id)
        {
            var entity = _db.EMPLOYMENT_STATUS.FirstOrDefault(x => x.employmentStatusID == id && !x.isDeleted);
            if (entity == null) return null;

            var employmentStatus = new EmploymentStatus(entity.employmentStatus);
            employmentStatus.SetId(entity.employmentStatusID);
            return employmentStatus;
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
