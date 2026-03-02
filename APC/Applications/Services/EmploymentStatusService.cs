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
    public class EmploymentStatusService : IEmploymentStatusService
    {
        private readonly IEmploymentStatusRepository _repository;
        public EmploymentStatusService(IEmploymentStatusRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(EmploymentStatus employmentStatus)
        {
            if (_repository.Exists(employmentStatus.EmploymentStatusName))
                throw new Exception("Employment status already exists");

            return _repository.Insert(employmentStatus);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<EmploymentStatus> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(EmploymentStatus employmentStatus)
        {
            var check = _repository.GetById(employmentStatus.EmploymentStatusId);
            if (check == null)
                throw new Exception("Employment status not found");

            employmentStatus.UpdateName(employmentStatus.EmploymentStatusName);
            return _repository.Update(employmentStatus);
        }
    }
}
