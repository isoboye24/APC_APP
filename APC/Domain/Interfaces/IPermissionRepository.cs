using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface IPermissionRepository
    {
        List<Permission> GetAll();
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
