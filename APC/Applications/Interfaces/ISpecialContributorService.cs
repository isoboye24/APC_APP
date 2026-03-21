using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Applications.Interfaces
{
    public interface ISpecialContributorService
    {
        List<SpecialContributor> GetAll();
        bool Create(SpecialContributor data);
        bool Update(SpecialContributor data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();

        List<SpecialContributor> GetAllDeleted();
        List<SpecialContributorFullDetails> GetFullSpecialContributorFullDetails();
    }
}
