using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Domain.Interfaces
{
    public interface IPositionRepository
    {
        IQueryable<POSITION> GetAll();
        IQueryable<POSITION> GetAllDeletedPositions();
        Position GetById(int id);
        bool Insert(Position position);
        bool Update(Position position);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
