using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface ISpecialContributorRepository
    {
        List<SpecialContributor> GetAll();
        SpecialContributor GetById(int id);
        bool Insert(SpecialContributor data);
        bool Update(SpecialContributor data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(int memberId, int specialContributionId);
        int Count();
        List<SpecialContributor> GetAllDeleted();
        List<SpecialContributorFullDetails> GetFullSpecialContributorDetails();
    }
}
