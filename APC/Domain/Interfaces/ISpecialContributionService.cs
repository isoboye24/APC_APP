using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface ISpecialContributionService
    {
        List<SpecialContribution> GetAll();
        bool Create(SpecialContribution data);
        bool Update(SpecialContribution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        List<SpecialContribution> GetAllDeleted();
        List<SpecialContributionFullDetails> GetFullSpecialContributionDetails();
    }
}
