using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class NationalityService : INationalityService
    {
        private readonly INationalityRepository _repository;
        public NationalityService(INationalityRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(Nationality nationality)
        {
            if (_repository.Exists(nationality.NationalityName))
                throw new Exception("Nationality already exists");

            return _repository.Insert(nationality);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<NationalityDTO> GetAll()
        {
            return _repository.GetAll()
                 .ToList()
                .Select(x => new NationalityDTO
                {
                    NationalityId = x.nationalityID,
                    NationalityName = x.nationality1
                }).OrderBy(x => x.NationalityName)
                .ToList();
        }
        public List<NationalityDTO> GetAllDeletedNationalities()
        {
            return _repository.GetAllDeletedNationalities()
                 .ToList()
                .Select(x => new NationalityDTO
                {
                    NationalityId = x.nationalityID,
                    NationalityName = x.nationality1
                }).OrderBy(x => x.NationalityName)
                .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Nationality nationality)
        {
            var check = _repository.GetById(nationality.NationalityId);
            if (check == null)
                throw new Exception("Nationality not found");

            else
            {
                return _repository.Update(nationality);                
            }
        }
    }
}
