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
    public class MaritalStatusService : IMaritalStatusService
    {
        private readonly IMaritalStatusRepository _repository;
        public MaritalStatusService(IMaritalStatusRepository repository)
        {
            _repository = repository;
        }
        public int Count()
            => _repository.Count();

        public bool Create(MaritalStatus maritalStatus)
        {
            if (_repository.Exists(maritalStatus.MaritalStatusName))
                throw new Exception("Marital status already exists");

            return _repository.Insert(maritalStatus);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<MaritalStatus> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(MaritalStatus maritalStatus)
        {
            var check = _repository.GetById(maritalStatus.MaritalStatusId);
            if (check == null)
                throw new Exception("Marital status not found");

            maritalStatus.UpdateName(maritalStatus.MaritalStatusName);
            return _repository.Update(maritalStatus);
        }
    }
}
