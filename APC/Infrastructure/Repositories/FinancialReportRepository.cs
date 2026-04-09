using APC.Applications.Interfaces;
using APC.DAL;
using APC.Domain.Entities;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class FinancialReportRepository : IFinancialReportRepository
    {
        private readonly APCEntities _db;
        public FinancialReportRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.FINANCIAL_REPORT.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.FINANCIAL_REPORT.First(x => x.financialReportID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(int year)
        {
            return _db.FINANCIAL_REPORT.Any(x => !x.isDeleted && x.year == year);
        }

        public IQueryable<FINANCIAL_REPORT> GetAll()
        {
            return _db.FINANCIAL_REPORT.Where(x => !x.isDeleted);
        }
        
        public IQueryable<FINANCIAL_REPORT> GetAllDeletedFinancialReports()
        {
            return _db.FINANCIAL_REPORT.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.FINANCIAL_REPORT.First(x => x.financialReportID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<FINANCIAL_REPORT> GetById(int id)
        {
            return _db.FINANCIAL_REPORT.Where(x => !x.isDeleted && x.financialReportID == id);
        }

        public bool Insert(FinancialReport data)
        {
            _db.FINANCIAL_REPORT.Add(new FINANCIAL_REPORT
            {
                totalAmountRaised = data.TotalAmountRaised,
                totalAmountSpent = data.TotalAmountSpent,
                year = data.Year,
                summary = data.Summary,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.FINANCIAL_REPORT.FirstOrDefault(x => x.financialReportID == id);

            if (entity == null)
                return false;

            _db.FINANCIAL_REPORT.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(FinancialReport data)
        {
            var entity = _db.FINANCIAL_REPORT.First(x => x.financialReportID == data.FinancialReportId);
            entity.totalAmountRaised = data.TotalAmountRaised;
            entity.totalAmountSpent = data.TotalAmountSpent;
            entity.year = data.Year;
            entity.summary = data.Summary;

            _db.SaveChanges();
            return true;
        }
    }
}
