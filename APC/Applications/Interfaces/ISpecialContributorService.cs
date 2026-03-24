using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface ISpecialContributorService
    {
        List<SpecialContributorDTO> GetAll();
        List<SpecialContributorDTO> GetAllDeletedSpecialContributors();
        bool Create(SpecialContributor data);
        bool Update(SpecialContributor data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
        decimal GetByAmountContributedByContributionId(int id);
    }
}
