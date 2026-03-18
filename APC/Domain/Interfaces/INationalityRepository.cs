using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Domain.Interfaces
{
    public interface INationalityRepository
    {
        IQueryable<NATIONALITY>GetAll();
        IQueryable<NATIONALITY>GetAllDeletedNationalities();
        Nationality GetById(int id);
        bool Insert(Nationality nationality);
        bool Update(Nationality nationality);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
