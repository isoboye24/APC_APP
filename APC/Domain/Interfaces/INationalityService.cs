using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface INationalityService
    {
        List<NationalityDTO> GetAll();
        bool Create(Nationality nationality);
        bool Update(Nationality nationality);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
