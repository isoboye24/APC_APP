using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace APC.Applications.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repository;
        public PermissionService(IPermissionRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(Permission permission)
        {
            if (_repository.Exists(permission.PermissionName))
                throw new Exception("Permission already exists");

            return _repository.Insert(permission);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<PermissionDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new PermissionDTO
                {
                    PermissionId = x.permissionID,
                    PermissionName = x.permission1
                }).OrderBy(x => x.PermissionName)
                .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Permission permission)
        {
            var check = _repository.GetById(permission.PermissionId);
            if (check == null)
                throw new Exception("Permission not found");

            permission.UpdateName(permission.PermissionName);
            return _repository.Update(permission);
        }
    }
}
