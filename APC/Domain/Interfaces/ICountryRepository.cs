using APC.AllForms.ViewModels;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface ICountryRepository
    {
        List<CountryViewModel> GetAll();
        Country GetById(int id);
        bool Insert(Country country);
        bool Update(Country country);
        bool Delete(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
