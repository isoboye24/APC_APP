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
    public class ExpenditureBLL : IBLL<ExpenditureDTO, ExpenditureDetailDTO>
    {
        ExpenditureDAO dao = new ExpenditureDAO();
        MonthDAO monthDAO = new MonthDAO();
        public bool Delete(ExpenditureDetailDTO entity)
        {
            EXPENDITURE expenditure = new EXPENDITURE();
            expenditure.expenditureID = entity.ExpenditureID;
            return dao.Delete(expenditure);
        }

        public bool GetBack(ExpenditureDetailDTO entity)
        {
            return dao.GetBack(entity.ExpenditureID);
        }

        public bool Insert(ExpenditureDetailDTO entity)
        {
            EXPENDITURE expenditure = new EXPENDITURE();
            expenditure.summary = entity.Summary;
            expenditure.amountSpent = entity.AmountSpent;
            expenditure.day = entity.Day;
            expenditure.monthID = entity.MonthID;
            expenditure.year = Convert.ToInt32(entity.Year);
            expenditure.expenditureDate = entity.ExpenditureDate;
            return dao.Insert(expenditure);
        }

        public ExpenditureDTO Select()
        {
            ExpenditureDTO dto = new ExpenditureDTO();
            dto.Expenditures = dao.Select();
            dto.Months = monthDAO.Select();
            return dto;
        }

        public bool Update(ExpenditureDetailDTO entity)
        {
            EXPENDITURE expenditure = new EXPENDITURE();
            expenditure.expenditureID = entity.ExpenditureID;
            expenditure.summary = entity.Summary;
            expenditure.amountSpent = entity.AmountSpent;
            expenditure.day = entity.Day;
            expenditure.monthID = entity.MonthID;
            expenditure.year = Convert.ToInt32(entity.Year);
            expenditure.expenditureDate = entity.ExpenditureDate;
            return dao.Update(expenditure);
        }

        public decimal SelectTotalExpendituresYearly(int year)
        {
            return dao.SelectTotalExpendituresYearly(year);
        }
        public decimal SelectTotalExpenditures()
        {
            return dao.SelectTotalExpenditures();
        }
    }
}
