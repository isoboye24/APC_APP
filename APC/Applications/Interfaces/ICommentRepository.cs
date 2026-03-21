using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Interfaces
{
    public interface ICommentRepository
    {
        IQueryable<COMMENT> GetAll();
        IQueryable<COMMENT> GetAllDeletedComments();
        IQueryable<COMMENT> GetById(int id);
        bool Insert(Comment comment);
        bool Update(Comment comment);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string content, int memberId);
        int Count();
    }
}
