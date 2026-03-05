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
    public class ConstitutionService : IConstitutionService
    {
        private readonly IConstitutionRepository _repository;
        public ConstitutionService(IConstitutionRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(Constitution data)
        {
            if (_repository.Exists(data.ConstitutionText, data.Fine))
                throw new Exception("Constitution already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<Constitution> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Constitution data)
        {
            var check = _repository.GetById(data.ConstitutionId);
            if (check == null)
                throw new Exception("Constitution not found");

            data.UpdateConstitutionText(data.ConstitutionText);
            data.UpdateFine(data.Fine);
            data.UpdateSection(data.Section);
            data.UpdateConstitutionText(data.ConstitutionText);
            

            return _repository.Update(data);
        }
    }
}
