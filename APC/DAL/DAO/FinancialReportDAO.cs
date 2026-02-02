using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace APC.DAL.DAO
{
    public class FinancialReportDAO : APCContexts, IDAO<FinancialReportDetailDTO, FINANCIAL_REPORT>
    {
        public bool Delete(FINANCIAL_REPORT entity)
        {
            try
            {
                FINANCIAL_REPORT financialReport = db.FINANCIAL_REPORT.First(x=>x.financialReportID == entity.financialReportID);
                financialReport.isDeleted = true;
                financialReport.deletedDate = DateTime.Today;
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
                FINANCIAL_REPORT financialReport = db.FINANCIAL_REPORT.First(x=>x.financialReportID==ID);
                financialReport.isDeleted = false;
                financialReport.deletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(FINANCIAL_REPORT entity)
        {
            try
            {
                db.FINANCIAL_REPORT.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FinancialReportDetailDTO> Select()
        {
            try
            {
                List<FinancialReportDetailDTO> financialReport = new List<FinancialReportDetailDTO>();
                List<int> monthIDCollection = new List<int>();
                List<int> monthIDs = new List<int>();
                List<int> yearsCollection = new List<int>();
                List<int> years = new List<int>();

                List<decimal> totalDuesCollection = new List<decimal>();
                List<decimal> fines = new List<decimal>();
                List<decimal> totalEventSales = new List<decimal>();

                List<decimal> totalExpenditures = new List<decimal>();
                List<decimal> totalEventExpenses = new List<decimal>();

                var yearlyDue = db.FINANCIAL_REPORT.Where(x => x.isDeleted == false).ToList();
                foreach (var item in yearlyDue)
                {
                    yearsCollection.Add(item.year);
                }
                years = yearsCollection.Distinct().OrderByDescending(year => year).ToList();
                foreach (var yearItem in years)
                {
                    var report = db.FINANCIAL_REPORT.Where(x => x.isDeleted == false && x.year == yearItem).FirstOrDefault();
                    var yearlyDues = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.year == report.year).ToList();
                    foreach (var due in yearlyDues)
                    {
                        totalDuesCollection.Add((decimal)due.monthlyDues);
                    }
                    var allFines = db.FINED_MEMBER.Where(x => x.isdeleted == false && x.year == report.year).ToList();
                    foreach (var fine in allFines)
                    {
                        fines.Add((decimal)fine.amountPaid);
                    }
                    var allEventSales = db.EVENT_SALES.Where(x => x.isDeleted == false && x.year == report.year).ToList();
                    foreach (var eventSale in allEventSales)
                    {
                        totalEventSales.Add((decimal)eventSale.amountSold);
                    }


                    var yearlyExpenditures = db.EXPENDITURE.Where(x => x.isDeleted == false && x.year == report.year).ToList();
                    foreach (var expense in yearlyExpenditures)
                    {
                        totalExpenditures.Add(expense.amountSpent);
                    }
                    var allEventExpenses = db.EVENT_EXPENDITURE.Where(x => x.isDeleted == false && x.year == report.year).ToList();
                    foreach (var eventExp in allEventExpenses)
                    {
                        totalEventExpenses.Add(eventExp.amountSpent);
                    }
                                       
                    FinancialReportDetailDTO dto = new FinancialReportDetailDTO();
                    dto.FinancialReportID = report.financialReportID;
                    dto.Summary = report.summary;
                    dto.Year = report.year.ToString();
                    dto.TotalAmountRaised = totalDuesCollection.Sum() + fines.Sum() + totalEventSales.Sum();
                    dto.TotalAmountSpent = totalExpenditures.Sum() + totalEventExpenses.Sum();
                    dto.Balance = dto.TotalAmountRaised - dto.TotalAmountSpent;
                    financialReport.Add(dto);
                    totalDuesCollection.Clear();
                    totalExpenditures.Clear();
                    fines.Clear();
                    totalEventExpenses.Clear();
                    totalEventSales.Clear();
                }
                return financialReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FinancialReportDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<FinancialReportDetailDTO> financialReport = new List<FinancialReportDetailDTO>();
                var list = db.FINANCIAL_REPORT.Where(x => x.isDeleted == isDeleted);
                foreach (var item in list)
                {
                    FinancialReportDetailDTO dto = new FinancialReportDetailDTO();
                    dto.FinancialReportID = item.financialReportID;
                    dto.TotalAmountRaised = (decimal)item.totalAmountRaised;
                    dto.TotalAmountSpent = (decimal)item.totalAmountSpent;
                    dto.Year = item.year.ToString();
                    dto.Summary = item.summary;
                    financialReport.Add(dto);
                }
                return financialReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal SelectTotalRaisedAmount()
        {
            try
            {                
                List<decimal> totalRaisedAmount = new List<decimal>();
                List<decimal> fines = new List<decimal>();
                List<decimal> totalEventSold = new List<decimal>();
                var list = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false);
                foreach (var item in list)
                {                                     
                    totalRaisedAmount.Add((decimal)item.monthlyDues);
                }
                var allFines = db.FINED_MEMBER.Where(x => x.isdeleted == false).ToList();
                foreach (var fine in allFines)
                {
                    fines.Add((decimal)fine.amountPaid);
                }
                var allEvents = db.EVENT_SALES.Where(x => x.isDeleted == false).ToList();
                foreach (var events in allEvents)
                {
                    totalEventSold.Add((decimal)events.amountSold);
                }
                decimal totalAmount = totalRaisedAmount.Sum() + fines.Sum() + totalEventSold.Sum();
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal SelectTotalRaisedAmountYealy(int thisYear)
        {
            try
            {
                List<decimal> totalRaisedAmount = new List<decimal>();
                List<decimal> fines = new List<decimal>();
                var list = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.year == thisYear);
                foreach (var item in list)
                {
                    totalRaisedAmount.Add((decimal)item.monthlyDues);
                }
                var allFines = db.FINED_MEMBER.Where(x => x.isdeleted == false && x.year == thisYear).ToList();
                foreach (var fine in allFines)
                {
                    fines.Add((decimal)fine.amountPaid);
                }
                decimal totalAmount = totalRaisedAmount.Sum() + fines.Sum();
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal SelectTotalRaisedAmountMonthly(int thisMonth, int thisYear)
        {
            try
            {
                List<decimal> totalRaisedAmount = new List<decimal>();
                List<decimal> fines = new List<decimal>();
                var list = db.PERSONAL_ATTENDANCE.Where(x => x.isDeleted == false && x.monthID == thisMonth && x.year == thisYear);
                foreach (var item in list)
                {
                    totalRaisedAmount.Add((decimal)item.monthlyDues);
                }
                var allFines = db.FINED_MEMBER.Where(x => x.isdeleted == false && x.monthID == thisMonth && x.year == thisYear).ToList();
                foreach (var fine in allFines)
                {
                    fines.Add((decimal)fine.amountPaid);
                }
                decimal totalAmount = totalRaisedAmount.Sum();
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckTotalRaisedAmountAndTotalSpentAmount(int year)
        {
            try
            {
                List<decimal> totalRaisedAmount = new List<decimal>();
                var list = db.GENERAL_ATTENDANCE.Where(x => x.isDeleted == false && x.year == year);
                foreach (var item in list)
                {
                    totalRaisedAmount.Add((decimal)item.totalDuesPaid);
                }
                decimal totalAmountRaised = totalRaisedAmount.Sum();
                List<decimal> totalSpentAmount = new List<decimal>();
                var listSpent = db.EXPENDITURE.Where(x => x.isDeleted == false && x.year == year);
                foreach (var item in listSpent)
                {
                    totalSpentAmount.Add(item.amountSpent);
                }
                decimal totalAmountSpent = totalSpentAmount.Sum();
                if (totalAmountRaised > 0 || totalAmountSpent > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal SelectTotalSpentAmount()
        {
            try
            {
                List<decimal> totalSpentAmount = new List<decimal>();
                List<decimal> totalEventSpentAmount = new List<decimal>();

                var list = db.EXPENDITURE.Where(x => x.isDeleted == false);
                foreach (var item in list)
                {                                       
                    totalSpentAmount.Add(item.amountSpent);
                }
                var eventExpenses = db.EVENT_EXPENDITURE.Where(x => x.isDeleted == false);
                foreach (var eventExp in eventExpenses)
                {                                       
                    totalEventSpentAmount.Add(eventExp.amountSpent);
                }
                decimal totalAmount = totalSpentAmount.Sum() + totalEventSpentAmount.Sum();
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(FINANCIAL_REPORT entity)
        {
            try
            {
                FINANCIAL_REPORT financialReport = db.FINANCIAL_REPORT.First(x => x.financialReportID == entity.financialReportID);
                financialReport.summary = entity.summary;
                financialReport.year = entity.year;
                financialReport.totalAmountRaised = entity.totalAmountRaised;
                financialReport.totalAmountSpent = entity.totalAmountSpent;
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
