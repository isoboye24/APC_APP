using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly IExpenditureRepository _repository;
        public ExpenditureService(IExpenditureRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(Expenditure data)
        {
            if (_repository.Exists(data.AmountSpent, data.Summary, data.ExpenditureDate))
                throw new Exception("Expenditure already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<ExpenditureDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new ExpenditureDTO
                {
                    ExpenditureId = x.expenditureID,
                    AmountSpent = x.amountSpent,
                    Summary = x.summary,
                    ExpenditureDate = x.expenditureDate,
                    FormattedExpenditureDate = x.expenditureDate.ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.ExpenditureDate.Year)
                .ThenByDescending(x => x.ExpenditureDate.Month)
                .ThenByDescending(x => x.ExpenditureDate.Day)
                .ThenBy(x => x.Summary)
                .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(Expenditure data)
        {
            var check = _repository.GetById(data.ExpenditureId);
            if (check == null)
                throw new Exception("Expenditure not found");

            data.UpdateDate(data.ExpenditureDate);
            data.UpdateSpentAmount(data.AmountSpent);
            data.UpdateSummary(data.Summary);

            return _repository.Update(data);
        }
    }
}
