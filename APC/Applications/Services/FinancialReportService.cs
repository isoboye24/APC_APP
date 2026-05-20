using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace APC.Applications.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly IFinancialReportRepository _repository;
        public FinancialReportService(IFinancialReportRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(FinancialReport data)
        {
            if (_repository.Exists(data.Year))
                throw new Exception("Financial Report already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<FinancialReportDTO> GetAll()
        {
            return _repository.GetAll()
                 .Select(x => new FinancialReportDTO
                 {
                     FinancialReportId = x.financialReportID,
                     TotalAmountRaised = x.totalAmountRaised,
                     TotalAmountSpent = x.totalAmountSpent,
                     Year = x.year,
                     Summary = x.summary,
                 })
                 .OrderBy(x => x.Year)
                 .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(FinancialReport data)
        {
            var check = _repository.GetById(data.FinancialReportId);
            if (check == null)
                throw new Exception("Financial Report not found");

            data.UpdateTotalAmountRaised(data.TotalAmountRaised);
            data.UpdateTotalAmountSpent(data.TotalAmountSpent);
            data.UpdateYear(data.Year);
            data.UpdateSummary(data.Summary);

            return _repository.Update(data);
        }
    }
}
