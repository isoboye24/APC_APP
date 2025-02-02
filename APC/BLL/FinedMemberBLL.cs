using APC.DAL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class FinedMemberBLL : IBLL<FinedMemberDTO, FinedMemberDetailDTO>
    {
        ConstitutionDAO constDAO = new ConstitutionDAO();
        FinedMemberDAO dao = new FinedMemberDAO();
        MonthDAO monthDAO = new MonthDAO();
        MemberDAO memberDAO = new MemberDAO();
        GenderDAO genderDAO = new GenderDAO();
        public bool Delete(FinedMemberDetailDTO entity)
        {
            try
            {
                FINED_MEMBER finedMember = new FINED_MEMBER();
                finedMember.finedMemberID = entity.FinedMemberID;
                return dao.Delete(finedMember);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetBack(FinedMemberDetailDTO entity)
        {
            return dao.GetBack(entity.FinedMemberID);
        }

        public int SelectAllFinesCount(int ID)
        {
            return dao.SelectAllFinesCount(ID);
        }
        
        public decimal SelectTotalFinedExpected()
        {
            return dao.SelectTotalFinedExpected();
        }

        public decimal SelectTotalPaidFines()
        {
            return dao.TotalPaidFines();
        }

        public bool Insert(FinedMemberDetailDTO entity)
        {
            FINED_MEMBER finedMember = new FINED_MEMBER();
            finedMember.amountPaid = entity.AmountPaid;
            finedMember.constitutionID = entity.ConstitutionID;
            finedMember.summary = entity.Summary;
            finedMember.memberID = entity.MemberID;
            finedMember.day = entity.Day;
            finedMember.monthID = entity.MonthID;
            finedMember.year = entity.Year;
            return dao.Insert(finedMember);
        }

        public FinedMemberDTO Select()
        {
            FinedMemberDTO dto = new FinedMemberDTO();
            dto.Constitutions = constDAO.Select();
            dto.Members = memberDAO.Select();
            dto.Genders = genderDAO.Select();
            dto.Months = monthDAO.Select();
            dto.FineMembers = dao.Select();
            return dto;
        }

        public bool Update(FinedMemberDetailDTO entity)
        {
            FINED_MEMBER finedMember = new FINED_MEMBER();
            finedMember.finedMemberID = entity.FinedMemberID;
            finedMember.amountPaid = entity.AmountPaid;
            finedMember.constitutionID = entity.ConstitutionID;
            finedMember.summary = entity.Summary;
            finedMember.memberID = entity.MemberID;
            finedMember.day = entity.Day;
            finedMember.monthID = entity.MonthID;
            finedMember.year = entity.Year;
            return dao.Update(finedMember);
        }
    }
}
