using APC.DAL;
using APC.DAL.DAO;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly APCEntities _db;
        public PermissionRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.PERMISSION.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.PERMISSION.First(x => x.permissionID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string name)
        {
            return _db.PERMISSION.Any(x => !x.isDeleted && x.permission1 == name);
        }

        public List<Permission> GetAll()
        {
            var data = _db.PERMISSION
                .Where(x => !x.isDeleted)
                .OrderBy(x => x.permission1)
                .ToList();

            return data
                .Select(x => Permission.Rehydrate(
                    x.permissionID,
                    x.permission1
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.PERMISSION.First(x => x.permissionID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public Permission GetById(int id)
        {
            var entity = _db.PERMISSION.FirstOrDefault(x => x.permissionID == id && !x.isDeleted);
            if (entity == null) return null;

            var permission = new Permission(entity.permission1);
            permission.SetId(entity.permissionID);
            return permission;
        }

        public bool Insert(Permission permission)
        {
            _db.PERMISSION.Add(new PERMISSION
            {
                permission1 = permission.PermissionName
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.PERMISSION.FirstOrDefault(x => x.permissionID == id);

            if (entity == null)
                return false;

            _db.PERMISSION.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Permission permission)
        {
            var entity = _db.PERMISSION.First(x => x.permissionID == permission.PermissionId);
            entity.permission1 = permission.PermissionName;
            _db.SaveChanges();
            return true;
        }
    }
}
