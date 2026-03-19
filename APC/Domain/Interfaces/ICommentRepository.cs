using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Domain.Interfaces
{
    public interface ICommentRepository
    {
        List<CommentDTO> GetAll();
        List<CommentDTO> GetAllDeletedComments();
        Comment GetById(int id);
        bool Insert(Comment comment);
        bool Update(Comment comment);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string content, int memberId);
        int Count();
    }
}
