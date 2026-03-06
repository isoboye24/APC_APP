using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<FinancialReport> GetAll()
        {
            var data = _db.FINANCIAL_REPORT
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .ToList();

            return data
                .Select(x => FinancialReport.Rehydrate(
                    x.financialReportID,
                    x.totalAmountRaised,
                    x.totalAmountSpent,
                    x.year,
                    x.summary
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.FINANCIAL_REPORT.First(x => x.financialReportID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public FinancialReport GetById(int id)
        {
            var entity = _db.FINANCIAL_REPORT
                .Where(x => x.financialReportID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.financialReportID,
                    x.totalAmountRaised,
                    x.totalAmountSpent,
                    x.year,
                    x.summary
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return FinancialReport.Rehydrate(
                    entity.financialReportID,
                    entity.totalAmountRaised,
                    entity.totalAmountSpent,
                    entity.year,
                    entity.summary
            );
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
