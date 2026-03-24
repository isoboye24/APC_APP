using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface ISpecialContributionService
    {
        List<SpecialContributionDTO> GetAll();
        List<SpecialContributionDTO> GetAllDeletedSpecialContributions();
        bool Create(SpecialContribution data);
        bool Update(SpecialContribution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
