using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<FinancialReport> GetAll()
            => _repository.GetAll();

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
