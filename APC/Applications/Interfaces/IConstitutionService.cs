using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IConstitutionService
    {
        List<ConstitutionDTO> GetAll();
        List<ConstitutionDTO> GetAllDeletedConstitutions();
        ConstitutionDTO GetById(int id);
        bool Create(Constitution data);
        bool Update(Constitution data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
