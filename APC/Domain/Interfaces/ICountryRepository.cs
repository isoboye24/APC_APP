using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Domain.Interfaces
{
    public interface ICountryRepository
    {
        IQueryable<COUNTRY> GetAll();
        IQueryable<COUNTRY> GetAllDeletedCountries();
        Country GetById(int id);
        bool Insert(Country country);
        bool Update(Country country);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
