using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class CommentBLL : IBLL<CommentDTO, CommentDetailDTO>
    {
        MemberDAO memberDAO = new MemberDAO();
        GenderDAO genderDAO = new GenderDAO();
        CommentDAO commentDAO = new CommentDAO();
        MonthDAO monthDAO = new MonthDAO();
        public bool Delete(CommentDetailDTO entity)
        {
            COMMENT comment = new COMMENT();
            comment.commentID = entity.CommentID;
            return commentDAO.Delete(comment);
        }

        public bool GetBack(CommentDetailDTO entity)
        {
            return commentDAO.GetBack(entity.CommentID);
        }

        public bool Insert(CommentDetailDTO entity)
        {
            COMMENT comment = new COMMENT();
            comment.comment1 = entity.CommentName;
            comment.memberID = entity.MemberID;
            comment.day = entity.Day;
            comment.monthID = entity.MonthID;
            comment.year = Convert.ToInt32(entity.Year);
            return commentDAO.Insert(comment);
        }

        public CommentDTO Select()
        {
            CommentDTO dto = new CommentDTO();
            dto.Members = memberDAO.Select();
            dto.Genders = genderDAO.Select();
            dto.Months = monthDAO.Select();
            dto.Comments = commentDAO.Select();
            return dto;
        }
        
        public CommentDTO Select(bool isDeleted)
        {
            CommentDTO dto = new CommentDTO();
            dto.Comments = commentDAO.Select(isDeleted);
            return dto;
        }
        public CommentDTO SelectMembersCommentList(int ID)
        {
            CommentDTO dto = new CommentDTO();            
            dto.Comments = commentDAO.SelectMembersCommentList(ID);
            return dto;
        }

        public bool Update(CommentDetailDTO entity)
        {
            COMMENT comment = new COMMENT();
            comment.commentID = entity.CommentID;
            comment.comment1 = entity.CommentName;
            comment.memberID = entity.MemberID;
            comment.day = entity.Day;
            comment.monthID = entity.MonthID;
            comment.year = Convert.ToInt32(entity.Year);
            return commentDAO.Update(comment);
        }
        public int SelectAllCommentsCount()
        {
            return commentDAO.SelectAllCommentsCount();
        }
        public int SelectMonthlyCommentsCount()
        {
            return commentDAO.SelectMonthlyCommentsCount();
        }
    }
}
