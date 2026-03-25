using APC.DAL;
using APC.Domain.Entities;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface ISpecialContributorRepository
    {
        IQueryable<SPECIAL_CONTRIBUTORS> GetAllByContributionId(int id);
        IQueryable<SPECIAL_CONTRIBUTORS> GetAllDeletedContributors();
        IQueryable<SPECIAL_CONTRIBUTORS> GetById(int id);
        bool Insert(SpecialContributor data);
        bool Update(SpecialContributor data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int memberId, int specialContributionId);
        int Count();
        decimal GetAmountContributedByContributionId(int id);
    }
}
