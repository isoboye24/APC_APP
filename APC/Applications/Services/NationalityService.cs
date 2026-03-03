using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Nationality> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Nationality nationality)
        {
            var check = _repository.GetById(nationality.NationalityId);
            if (check == null)
                throw new Exception("Nationality not found");

            nationality.UpdateName(nationality.NationalityName);
            return _repository.Update(nationality);
        }
    }
}
