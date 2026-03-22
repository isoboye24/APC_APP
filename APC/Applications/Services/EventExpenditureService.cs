using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class EventExpenditureService : IEventExpenditureService
    {
        private readonly IEventExpenditureRepository _repository;

        public EventExpenditureService(IEventExpenditureRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(EventExpenditure data)
        {
            if (_repository.Exists(data.EventId, data.SpentAmount, data.Summary, data.ExpenditureDate))
                throw new Exception("Event expenditure already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<EventExpenditure> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(EventExpenditure data)
        {
            var check = _repository.GetById(data.EventExpenditureId);
            if (check == null)
                throw new Exception("Event Expenditure not found");

            data.UpdateDate(data.ExpenditureDate);
            data.UpdateSpentAmount(data.SpentAmount);
            data.UpdateSummary(data.Summary);

            return _repository.Update(data);
        }
    }
}
