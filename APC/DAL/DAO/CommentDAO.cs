using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.DAL.DAO
{
    public class CommentDAO : APCContexts, IDAO<CommentDetailDTO, COMMENT>
    {
        public bool Delete(COMMENT entity)
        {
            try
            {
                COMMENT comment = db.COMMENT.First(x => x.commentID == entity.commentID);
                comment.isDeleted = true;
                comment.deletedDate = DateTime.Today;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public bool GetBack(int ID)
        {
            try
            {
                COMMENT comment = db.COMMENT.First(x=>x.commentID == ID);
                comment.isDeleted = false;
                comment.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(COMMENT entity)
        {
            try
            {
                db.COMMENT.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommentDetailDTO> Select()
        {
            try
            {
                List<CommentDetailDTO> comments = new List<CommentDetailDTO>();
                var list = (from c in db.COMMENT.Where(x => x.isDeleted == false)
                            join mo in db.MONTH on c.monthID equals mo.monthID
                            join m in db.MEMBER on c.memberID equals m.memberID
                            join g in db.GENDER on m.genderID equals g.genderID
                            select new
                            {
                                commentID = c.commentID,
                                comment = c.comment1,
                                surname = m.surname,
                                name = m.name,
                                genderName = g.genderName,
                                day = c.day,
                                monthID = c.monthID,
                                monthName = mo.monthName,
                                year = c.year,
                                imagePath = m.imagePath,
                                memberID = c.memberID,
                                genderID = m.genderID
                            }).OrderByDescending(x => x.monthID).ToList();
                foreach (var item in list)
                {
                    CommentDetailDTO dto = new CommentDetailDTO();
                    dto.CommentID = item.commentID;
                    dto.CommentName = item.comment;
                    dto.Surname = item.surname;
                    dto.Name = item.name;
                    dto.GenderName = item.genderName;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.ImagePath = item.imagePath;
                    dto.MemberID = item.memberID;
                    dto.GenderID = item.genderID;
                    comments.Add(dto);
                }
                return comments;
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }
        public List<CommentDetailDTO> SelectMembersCommentList(int ID)
        {
            try
            {
                List<CommentDetailDTO> comments = new List<CommentDetailDTO>();
                var list = (from c in db.COMMENT.Where(x => x.isDeleted == false && x.memberID == ID)
                            join mo in db.MONTH on c.monthID equals mo.monthID
                            join m in db.MEMBER on c.memberID equals m.memberID
                            join g in db.GENDER on m.genderID equals g.genderID
                            select new
                            {
                                commentID = c.commentID,
                                comment = c.comment1,
                                surname = m.surname,
                                name = m.name,
                                genderName = g.genderName,
                                day = c.day,
                                monthID = c.monthID,
                                monthName = mo.monthName,
                                year = c.year,
                                imagePath = m.imagePath,
                                memberID = c.memberID,
                                genderID = m.genderID
                            }).OrderByDescending(x => x.year).ThenByDescending(x=>x.monthID).ThenByDescending(x=>x.day).ToList();
                foreach (var item in list)
                {
                    CommentDetailDTO dto = new CommentDetailDTO();
                    dto.CommentID = item.commentID;
                    dto.CommentName = item.comment;
                    dto.Surname = item.surname;
                    dto.Name = item.name;
                    dto.GenderName = item.genderName;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.ImagePath = item.imagePath;
                    dto.MemberID = item.memberID;
                    dto.GenderID = item.genderID;
                    comments.Add(dto);
                }
                return comments;
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }
        public List<CommentDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<CommentDetailDTO> comments = new List<CommentDetailDTO>();
                var list = (from c in db.COMMENT.Where(x => x.isDeleted == isDeleted)
                            join mo in db.MONTH on c.monthID equals mo.monthID
                            join m in db.MEMBER.Where(x => x.isDeleted == false) on c.memberID equals m.memberID
                            join g in db.GENDER on m.genderID equals g.genderID
                            select new
                            {
                                commentID = c.commentID,
                                comment = c.comment1,
                                surname = m.surname,
                                name = m.name,
                                genderName = g.genderName,
                                day = c.day,
                                monthID = c.monthID,
                                monthName = mo.monthName,
                                year = c.year,
                                imagePath = m.imagePath,
                                memberID = c.memberID,
                                genderID = m.genderID,
                                isMemberDeleted = m.isDeleted,
                            }).OrderByDescending(x => x.monthID).ToList();
                foreach (var item in list)
                {
                    CommentDetailDTO dto = new CommentDetailDTO();
                    dto.CommentID = item.commentID;
                    dto.CommentName = item.comment;
                    dto.Surname = item.surname;
                    dto.Name = item.name;
                    dto.GenderName = item.genderName;
                    dto.Day = item.day;
                    dto.MonthID = item.monthID;
                    dto.MonthName = item.monthName;
                    dto.Year = item.year.ToString();
                    dto.ImagePath = item.imagePath;
                    dto.MemberID = item.memberID;
                    dto.GenderID = item.genderID;
                    dto.isMemberDeleted = item.isMemberDeleted;
                    comments.Add(dto);
                }
                return comments;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int SelectAllCommentsCount()
        {
            try
            {
                int totalComments = db.COMMENT.Count(x => x.isDeleted == false);
                return totalComments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SelectMonthlyCommentsCount()
        {            
            try
            {
                int totalComments = db.COMMENT.Count(x => x.isDeleted == false && x.monthID == DateTime.Today.Month);
                return totalComments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(COMMENT entity)
        {
            try
            {
                COMMENT comment = db.COMMENT.First(x => x.commentID == entity.commentID);
                comment.comment1 = entity.comment1;
                comment.memberID = entity.memberID;
                comment.day = entity.day;
                comment.monthID = entity.monthID;
                comment.year = entity.year;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
