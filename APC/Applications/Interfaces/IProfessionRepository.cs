using APC.Infrastructure.Data;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IProfessionRepository
    {
        IQueryable<PROFESSION> GetAll();
        IQueryable<PROFESSION> GetAllDeletedProfessions();
        Profession GetById(int id);
        bool Insert(Profession profession);
        bool Update(Profession profession);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
