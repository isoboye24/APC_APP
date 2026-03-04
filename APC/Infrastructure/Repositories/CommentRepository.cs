using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public bool Exists(string content)
        {
            return _db.COMMENT.Any(x => !x.isDeleted && x.comment1 == content);
        }

        public List<Comment> GetAll()
        {
            var data = _db.COMMENT
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .ThenByDescending(x => x.monthID)
                .ThenByDescending(x => x.day)
                .ThenBy(x => x.memberID)
                .ToList();

            return data
                .Select(x => Comment.Rehydrate(
                    x.commentID,
                    x.comment1,
                    x.memberID,
                    x.day,
                    x.monthID,
                    x.year
                ))
                .ToList();
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
