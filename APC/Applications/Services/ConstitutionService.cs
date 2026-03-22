using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<ConstitutionDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new ConstitutionDTO
                {
                    ConstitutionId = x.constitutionID,
                    ConstitutionText = x.constitution1,
                    Fine = x.fine,
                    FineWithCurrency = (x.fine + " €").ToString(),
                    Section = x.section,
                    ShortDescription = x.ShortDescription,
                })
                .OrderBy(x => x.ShortDescription)
                .ToList();
        }
        
        public List<ConstitutionDTO> GetAllDeletedConstitutions()
        {
            return _repository.GetAllDeletedConstitutions()
                .Select(x => new ConstitutionDTO
                {
                    ConstitutionId = x.constitutionID,
                    ConstitutionText = x.constitution1,
                    Fine = x.fine,
                    FineWithCurrency = (x.fine + " €").ToString(),
                    Section = x.section,
                    ShortDescription = x.ShortDescription,
                })
                .OrderBy(x => x.ShortDescription)
                .ToList();
        }
        
        public ConstitutionDTO GetById(int id)
        {
            return _repository.GetById(id)
                .Select(x => new ConstitutionDTO
                {
                    ConstitutionId = x.constitutionID,
                    ConstitutionText = x.constitution1,
                    Fine = x.fine,
                    FineWithCurrency = (x.fine + " €").ToString(),
                    Section = x.section,
                    ShortDescription = x.ShortDescription,
                })
                .OrderBy(x => x.ShortDescription)
                .FirstOrDefault();
        }

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
