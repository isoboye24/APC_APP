using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface ISpecialContributionRepository
    {
        IQueryable<SPECIAL_CONTRIBUTIONS> GetAll();
        IQueryable<SPECIAL_CONTRIBUTIONS> GetAllDeletedSpecialContributions();
        IQueryable<SPECIAL_CONTRIBUTIONS> GetById(int id);
        bool Insert(SpecialContribution data);
        bool Update(SpecialContribution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string title);
        int Count();
    }
}
