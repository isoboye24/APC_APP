using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface ISpecialContributionRepository
    {
        List<SpecialContribution> GetAll();
        SpecialContribution GetById(int id);
        bool Insert(SpecialContribution data);
        bool Update(SpecialContribution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string title);
        int Count();
        List<SpecialContribution> GetAllDeleted();
        List<SpecialContributionFullDetails> GetFullSpecialContributionDetails();
    }
}
