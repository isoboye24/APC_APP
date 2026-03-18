using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IPermissionService
    {
        List<PermissionDTO> GetAll();
        bool Create(Permission permission);
        bool Update(Permission permission);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
