using APC.Domain.Entities;
using System;
using System.Collections.Generic;


namespace APC.Applications.Interfaces
{
    public interface IConstitutionRepository
    {
        List<Constitution> GetAll();
        Constitution GetById(int id);
        bool Insert(Constitution data);
        bool Update(Constitution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string constitutionText, decimal fine);
        int Count();
    }
}
