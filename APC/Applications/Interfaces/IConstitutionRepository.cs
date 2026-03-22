using APC.DAL;
using APC.Domain.Entities;
using System.Linq;


namespace APC.Applications.Interfaces
{
    public interface IConstitutionRepository
    {
        IQueryable<CONSTITUTION> GetAll();
        IQueryable<CONSTITUTION> GetAllDeletedConstitutions();
        IQueryable<CONSTITUTION> GetById(int id);
        IQueryable<CONSTITUTION> GetBySection(string section);

        bool Insert(Constitution data);
        bool Update(Constitution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string constitutionText, decimal fine);
        int Count();
    }
}
