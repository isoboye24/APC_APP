using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface IMemberRepository
    {
        IQueryable<MEMBER> GetAll();
        IQueryable<MEMBER> GetAllDeletedMembers();
        MEMBER GetById(int id);
        bool Insert(Member data);
        bool Update(Member data);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string firstName, string lastName);
        MEMBER GetByUsername(string username);
        int Count();
    }
}
