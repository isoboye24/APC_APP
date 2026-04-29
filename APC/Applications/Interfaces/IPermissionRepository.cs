using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IPermissionRepository
    {
        IQueryable<PERMISSION> GetAll();
        IQueryable<PERMISSION> GetAllDeletedPermissions();

        IQueryable<PERMISSION> GetByStatus(string status);
        IQueryable<PERMISSION> GetAllStatusExcept(string status);
        Permission GetById(int id);
        bool Insert(Permission permission);
        bool Update(Permission permission);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
