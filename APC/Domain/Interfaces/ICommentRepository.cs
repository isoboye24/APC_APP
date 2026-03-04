using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
{
    public interface ICommentRepository
    {
        List<Comment> GetAll();
        Comment GetById(int id);
        bool Insert(Comment comment);
        bool Update(Comment comment);
        bool Delete(int id);
        bool GetBack(int id);
        bool PermanentDelete(int id);
        bool Exists(string content);
        int Count();
    }
}
