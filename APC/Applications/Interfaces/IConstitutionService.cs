using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IConstitutionService
    {
        List<Constitution> GetAll();
        bool Create(Constitution data);
        bool Update(Constitution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
