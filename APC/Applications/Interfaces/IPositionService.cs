using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface IPositionService
    {
        List<PositionDTO> GetAll();
        List<PositionDTO> GetAllDeletedPositions();
        bool Create(Position position);
        bool Update(Position position);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
