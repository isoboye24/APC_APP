using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class ProfessionService : IProfessionService
    {
        private readonly IProfessionRepository _repository;
        public ProfessionService(IProfessionRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(Profession profession)
        {
            if (_repository.Exists(profession.ProfessionName))
                throw new Exception("Profession already exists");

            return _repository.Insert(profession);
        }

        public bool Delete(int id)
             => _repository.Delete(id);

        public List<ProfessionDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new ProfessionDTO
                {
                    ProfessionId = x.professionID,
                    ProfessionName = x.profession1
                }).OrderBy(x => x.ProfessionName)
                .ToList();
        }
        
        public List<ProfessionDTO> GetAllDeletedProfessions()
        {
            return _repository.GetAllDeletedProfessions()
                .Select(x => new ProfessionDTO
                {
                    ProfessionId = x.professionID,
                    ProfessionName = x.profession1
                }).OrderBy(x => x.ProfessionName)
                .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Profession profession)
        {
            var check = _repository.GetById(profession.ProfessionId);
            if (check == null)
                throw new Exception("Profession not found");

            profession.UpdateName(profession.ProfessionName);
            return _repository.Update(profession);
        }
    }
}
