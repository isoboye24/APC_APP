using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace APC.Applications.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(Comment content)
        {
            if (_repository.Exists(content.Content, content.MemberId))
                throw new Exception("Comment already exists");

            return _repository.Insert(content);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<CommentDetailDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new CommentDetailDTO
                {
                    CommentId = x.CommentId,
                    MemberId = x.MemberId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                    Content = x.Content,
                    Gender = x.Gender,
                    Date = new DateTime(x.Year, x.MonthId, x.Day),
                    FormattedDate = new DateTime(x.Year, x.MonthId, x.Day).ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.Date.Year)
                .ThenByDescending(x => x.Date.Month)
                .ThenByDescending(x => x.Date.Day)
                .ThenBy(x => x.FirstName)
                .ToList();
        }
        
        public List<CommentDetailDTO> GetAllDeletedComments()
        {
            return _repository.GetAllDeletedComments()
                .Select(x => new CommentDetailDTO
                {
                    CommentId = x.CommentId,
                    MemberId = x.MemberId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImagePath = x.ImagePath,
                    Content = x.Content,
                    Gender = x.Gender,
                    Date = new DateTime(x.Year, x.MonthId, x.Day),
                    FormattedDate = new DateTime(x.Year, x.MonthId, x.Day).ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.Date.Year)
                .ThenByDescending(x => x.Date.Month)
                .ThenByDescending(x => x.Date.Day)
                .ThenBy(x => x.FirstName)
                .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Comment comment)
        {
            var check = _repository.GetById(comment.CommentId);
            if (check == null)
                throw new Exception("Comment not found");

            comment.UpdateContent(comment.Content);
            comment.ChangeMember(comment.MemberId);
            comment.ChangeDate(comment.Day, comment.MonthId, comment.Year);

            return _repository.Update(comment);
        }
    }
}
