using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class EventExpenditureService : IEventExpenditureService
    {
        private readonly IEventExpenditureRepository _repository;
        private readonly IEventsRepository _eventRepository;

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

        public List<EventExpenditureDTO> GetAll(int eventId)
        {
            var data = (from ex in _repository.GetAll(eventId)
                        join e in _eventRepository.GetAll() on ex.eventID equals e.eventID                        
                        select new EventExpenditureDTO
                        {
                            EventExpenditureId = ex.eventExpenditureID,
                            EventId = e.eventID,
                            SpentAmount = ex.amountSpent,
                            ExpenditureDate = e.eventDate,
                            Summary = ex.summary,
                            
                        }).OrderByDescending(x => x.ExpenditureDate.Year).ThenByDescending(x => x.ExpenditureDate.Month).ThenByDescending(x => x.ExpenditureDate.Day).ThenByDescending(x => x.SpentAmount).ToList();

            return data;
        }

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
