using APC.Applications.DTO;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Domain.Interfaces
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
