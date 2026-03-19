using APC.Applications.DTO;
using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        public Comment GetById(int id)
        {
            var entity = _db.COMMENT
                .Where(x => x.commentID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.commentID,
                    x.comment1,
                    x.memberID,
                    x.day,
                    x.monthID,
                    x.year
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return Comment.Rehydrate(
                entity.commentID,
                entity.comment1,
                entity.memberID,
                entity.day,
                entity.monthID,
                entity.year
            );
        }
        

        public bool Insert(Comment comment)
        {
            _db.COMMENT.Add(new COMMENT
            {
                comment1 = comment.Content,
                memberID = comment.MemberId,
                day = comment.Day,
                monthID = comment.MonthId,
                year = comment.Year
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
            entity.day = comment.Day;
            entity.monthID = comment.MonthId;
            entity.year = comment.Year;
            _db.SaveChanges();
            return true;
        }
    }
}
