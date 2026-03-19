using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface IProfessionService
    {
        List<ProfessionDTO> GetAll();
        List<ProfessionDTO> GetAllDeletedProfessions();
        bool Create(Profession profession);
        bool Update(Profession profession);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
