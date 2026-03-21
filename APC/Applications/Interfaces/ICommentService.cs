using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;

namespace APC.Applications.Interfaces
{
    public interface ICommentService
    {
        List<CommentDTO> GetAll();
        List<CommentDTO> GetAllDeletedComments();
        bool Create(Comment content);
        bool Update(Comment content);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        int Count();
    }
}
