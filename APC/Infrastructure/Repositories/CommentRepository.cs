using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly APCEntities _db;
        public CommentRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.COMMENT.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.COMMENT.First(x => x.commentID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string content, int memberId)
        {
            return _db.COMMENT.Any(x => !x.isDeleted && x.comment1 == content && x.memberID == memberId);
        }

        public List<CommentDTO> GetAll()
        {
            var member = (from c in _db.COMMENT.Where(x => x.isDeleted == false)
                          join m in _db.MEMBER on c.memberID equals m.memberID
                          join g in _db.GENDER on m.genderID equals g.genderID
                          select new CommentDTO
                          {
                              CommentId = c.commentID,
                              MemberId = c.memberID,
                              Content = c.comment1,
                              FirstName = m.name,
                              LastName = m.surname,
                              GenderId = m.genderID,
                              Gender = g.genderName,
                              ImagePath = m.imagePath,
                              Date = new DateTime(c.year, c.monthID, c.day),
                              FormattedDate = new DateTime(c.year, c.monthID, c.day).ToString("dd.MM.yyyy"),
                          });
            
            return member.OrderByDescending(x => x.Date.Year).ThenByDescending(x => x.Date.Month).ThenByDescending(x => x.Date.Day).ThenBy(x => x.FirstName).ToList();
        }
        
        public List<CommentDTO> GetAllDeletedComments()
        {
            var member = (from c in _db.COMMENT.Where(x => x.isDeleted)
                          join m in _db.MEMBER on c.memberID equals m.memberID
                          join g in _db.GENDER on m.genderID equals g.genderID
                          select new CommentDTO
                          {
                              CommentId = c.commentID,
                              MemberId = c.memberID,
                              Content = c.comment1,
                              FirstName = m.name,
                              LastName = m.surname,
                              Gender = g.genderName,
                              GenderId = m.genderID,
                              ImagePath = m.imagePath,
                              Date = new DateTime(c.year, c.monthID, c.day),
                              FormattedDate = new DateTime(c.year, c.monthID, c.day).ToString("dd.MM.yyyy"),
                          });

            return member.OrderByDescending(x => x.Date.Year).ThenByDescending(x => x.Date.Month).ThenByDescending(x => x.Date.Day).ThenBy(x => x.FirstName).ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.COMMENT.First(x => x.commentID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public CommentDTO GetById(int id)
        {
           return (from c in _db.COMMENT.Where(x => !x.isDeleted && x.commentID == id)
             join m in _db.MEMBER on c.memberID equals m.memberID
             join g in _db.GENDER on m.genderID equals g.genderID
             select new CommentDTO
             {
                 CommentId = c.commentID,
                 MemberId = c.memberID,
                 Content = c.comment1,
                 FirstName = m.name,
                 LastName = m.surname,
                 GenderId = m.genderID,
                 Gender = g.genderName,
                 ImagePath = m.imagePath,
                 Date = new DateTime(c.year, c.monthID, c.day),
                 FormattedDate = new DateTime(c.year, c.monthID, c.day).ToString("dd.MM.yyyy"),
             }).FirstOrDefault();
        }
        

        public bool Insert(Comment comment)
        {
            _db.COMMENT.Add(new COMMENT
            {
                comment1 = comment.Content,
                memberID = comment.MemberId,
                day = comment.Date.Day,
                monthID = comment.Date.Month,
                year = comment.Date.Year
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.COMMENT.FirstOrDefault(x => x.commentID == id);

            if (entity == null)
                return false;

            _db.COMMENT.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Comment comment)
        {
            var entity = _db.COMMENT.First(x => x.commentID == comment.CommentId);
            entity.comment1 = comment.Content;
            entity.memberID = comment.MemberId;
            entity.day = comment.Date.Day;
            entity.monthID = comment.Date.Month;
            entity.year = comment.Date.Year;
            _db.SaveChanges();
            return true;
        }
    }
}
