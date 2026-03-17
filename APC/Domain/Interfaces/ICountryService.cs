using APC.AllForms.ViewModels;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface ICountryService
    {
        List<CountryViewModel> GetAll();
        bool Create(Country country);
        bool Update(Country country);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
