using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;
        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public List<CountryDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new CountryDTO
                {
                    CountryId = x.countryID,
                    CountryName = x.countryName
                })
                .OrderBy(x => x.CountryName)
                .ToList();
        }

        public List<CountryDTO> GetAllDeletedCountries()
        {
            return _repository.GetAllDeletedCountries()
                .Select(x => new CountryDTO
                {
                    CountryId = x.countryID,
                    CountryName = x.countryName
                })
                .OrderBy(x => x.CountryName)
                .ToList();
        }

        public int Count()
            => _repository.Count();

        public bool Create(Country country)
        {
            if (_repository.Exists(country.CountryName))
                throw new Exception("Country already exists");

            return _repository.Insert(country);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Country country)
        {
            var check = _repository.GetById(country.CountryId);
            if (check == null)
                throw new Exception("Country not found");

            country.UpdateName(country.CountryName);
            return _repository.Update(country);
        }
    }
}
