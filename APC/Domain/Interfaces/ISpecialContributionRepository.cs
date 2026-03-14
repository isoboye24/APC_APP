using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
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
