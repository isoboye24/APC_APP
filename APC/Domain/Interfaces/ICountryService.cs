using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface ICountryService
    {
        List<CountryDTO> GetAll();
        List<CountryDTO> GetAllDeletedCountries();
        bool Create(Country country);
        bool Update(Country country);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
