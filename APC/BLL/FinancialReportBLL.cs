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
    public class FinancialReportBLL:IBLL<FinancialReportDTO, FinancialReportDetailDTO>
    {
        FinancialReportDAO dao = new FinancialReportDAO();
        ExpenditureDAO expDAO = new ExpenditureDAO();

        public bool Delete(FinancialReportDetailDTO entity)
        {
            FINANCIAL_REPORT financialReport = new FINANCIAL_REPORT();
            financialReport.financialReportID = entity.FinancialReportID;
            return dao.Delete(financialReport);
        }

        public bool GetBack(FinancialReportDetailDTO entity)
        {
            return dao.GetBack(entity.FinancialReportID);
        }

        public bool Insert(FinancialReportDetailDTO entity)
        {
            FINANCIAL_REPORT financialReport = new FINANCIAL_REPORT();
            financialReport.year = Convert.ToInt32(entity.Year);
            financialReport.summary = entity.Summary;
            financialReport.totalAmountRaised = entity.TotalAmountRaised;
            financialReport.totalAmountSpent = entity.TotalAmountSpent;
            return dao.Insert(financialReport);
        }

        public FinancialReportDTO Select()
        {
            FinancialReportDTO dto = new FinancialReportDTO();
            dto.FinancialReports = dao.Select();
            return dto;
        }
        public bool CheckTotalRaisedAmountAndTotalSpentAmount(int year)
        {
            return dao.CheckTotalRaisedAmountAndTotalSpentAmount(year);
        }

        public decimal SelectTotalRaisedAmount()
        {
            return dao.SelectTotalRaisedAmount();
        }
        public decimal SelectTotalRaisedAmountYearly(int year)
        {
            return dao.SelectTotalRaisedAmountYealy(year);
        }
        public decimal SelectTotalRaisedAmountMonthly(int month, int year)
        {
            return dao.SelectTotalRaisedAmountMonthly(month, year);
        }
        public decimal SelectTotalSpentAmount()
        {
            return dao.SelectTotalSpentAmount();
        }

        public decimal SelectTotalExpenditure()
        {
            return expDAO.SelectTotalExpenditures();
        }

        public decimal SelectTotalExpenditureYearly(int year)
        {
            return expDAO.SelectTotalExpenditureYearly(year);
        }

        public bool Update(FinancialReportDetailDTO entity)
        {
            FINANCIAL_REPORT financialReport = new FINANCIAL_REPORT();
            financialReport.financialReportID = entity.FinancialReportID;
            financialReport.year = Convert.ToInt32(entity.Year);
            financialReport.summary = entity.Summary;
            financialReport.totalAmountRaised = entity.TotalAmountRaised;
            financialReport.totalAmountSpent = entity.TotalAmountSpent;
            return dao.Update(financialReport);
        }
    }
}
