using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMemberService _memberRepo;
        private readonly IGenderService _genderRepo;
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
        {
            var data = (from c in _repository.GetAll()
                          join m in _memberRepo.GetAll() on c.memberID equals m.MemberId
                          join g in _genderRepo.GetAll() on m.PersonalInfo.GenderId equals g.GenderId
                          select new CommentDTO
                          {
                              CommentId = c.commentID,
                              MemberId = c.memberID,
                              Content = c.comment1,
                              FirstName = m.PersonalInfo.FirstName,
                              LastName = m.PersonalInfo.LastName,
                              GenderId = m.PersonalInfo.GenderId,
                              Gender = g.GenderName,
                              ImagePath = m.PersonalInfo.ImagePath,
                              Date = new DateTime(c.year, c.monthID, c.day),
                              FormattedDate = new DateTime(c.year, c.monthID, c.day).ToString("dd.MM.yyyy"),
                          }).OrderByDescending(x => x.Date.Year).ThenByDescending(x => x.Date.Month).ThenByDescending(x => x.Date.Day).ThenBy(x => x.FirstName).ToList();

            return data;
        }
            
        
        public List<CommentDTO> GetAllDeletedComments()
        {
            var data = (from c in _repository.GetAllDeletedComments()
                        join m in _memberRepo.GetAll() on c.memberID equals m.MemberId
                        join g in _genderRepo.GetAll() on m.PersonalInfo.GenderId equals g.GenderId
                        select new CommentDTO
                        {
                            CommentId = c.commentID,
                            MemberId = c.memberID,
                            Content = c.comment1,
                            FirstName = m.PersonalInfo.FirstName,
                            LastName = m.PersonalInfo.LastName,
                            GenderId = m.PersonalInfo.GenderId,
                            Gender = g.GenderName,
                            ImagePath = m.PersonalInfo.ImagePath,
                            Date = new DateTime(c.year, c.monthID, c.day),
                            FormattedDate = new DateTime(c.year, c.monthID, c.day).ToString("dd.MM.yyyy"),
                        }).ToList();

            return data;
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
            comment.ChangeDate(comment.Date);

            return _repository.Update(comment);
        }
    }
}
