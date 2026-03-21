using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IMaritalStatusRepository
    {
        IQueryable<MARITAL_STATUS> GetAll();
        IQueryable<MARITAL_STATUS> GetAllMaritalStatuses();
        MaritalStatus GetById(int id);
        bool Insert(MaritalStatus status);
        bool Update(MaritalStatus status);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string name);
        int Count();
    }
}
