using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
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

        public IQueryable<COMMENT> GetAll()
        {
            return _db.COMMENT.Where(x => !x.isDeleted);
        }

        public IQueryable<COMMENT> GetAllDeletedComments()
        {
            return _db.COMMENT.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.COMMENT.First(x => x.commentID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<COMMENT> GetById(int id)
        {
            return _db.COMMENT.Where(x => !x.isDeleted && x.commentID == id);
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
