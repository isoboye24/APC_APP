using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;

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

        public List<CommentDTO> GetAll()
            => _repository.GetAll();
            
        
        public List<CommentDTO> GetAllDeletedComments()
            => _repository.GetAllDeletedComments();

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
