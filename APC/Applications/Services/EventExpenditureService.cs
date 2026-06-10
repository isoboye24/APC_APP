using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class EventExpenditureService : IEventExpenditureService
    {
        private readonly IEventExpenditureRepository _repository;
        private readonly IEventsRepository _eventRepository;

        public EventExpenditureService(IEventExpenditureRepository repository, IEventsRepository eventRepository)
        {
            _repository = repository;
            _eventRepository = eventRepository;
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

        public List<EventExpenditureDTO> GetByEvent(int eventId)
        {
            var data = (from ex in _repository.GetAll().Where(x => x.eventID == eventId)
                        join e in _eventRepository.GetAll()
                            on ex.eventID equals e.eventID
                        select new
                        {
                            ex,
                            e
                        })
                .OrderByDescending(x => x.e.eventDate)
                .ThenByDescending(x => x.ex.amountSpent)
                .ToList()
                .Select((x, index) => new EventExpenditureDTO
                {
                    Counter = index + 1,
                    EventExpenditureId = x.ex.eventExpenditureID,
                    EventId = x.ex.eventID,
                    SpentAmount = x.ex.amountSpent,
                    FormattedSpentAmount = x.ex.amountSpent.ToString() + " €",
                    ExpenditureDate = x.e.eventDate,
                    FormattedExpenditureDate = x.e.eventDate.ToString("dd.MM.yyyy"),
                    Summary = x.ex.summary,
                })
                .ToList();

            return data;
        }

        public List<EventExpenditureDTO> GetAll()
        {
            var data = (from ex in _repository.GetAll()
                        join e in _eventRepository.GetAll()
                            on ex.eventID equals e.eventID
                        select new
                        {
                            ex,
                            e
                        })
                .OrderByDescending(x => x.e.eventDate)
                .ThenByDescending(x => x.ex.amountSpent)
                .ToList()
                .Select((x, index) => new EventExpenditureDTO
                {
                    Counter = index + 1,
                    EventExpenditureId = x.ex.eventExpenditureID,
                    EventId = x.ex.eventID,
                    SpentAmount = x.ex.amountSpent,
                    FormattedSpentAmount = x.ex.amountSpent.ToString() + " €",
                    ExpenditureDate = x.e.eventDate,
                    FormattedExpenditureDate = x.e.eventDate.ToString("dd.MM.yyyy"),
                    Summary = x.ex.summary,
                })
                .ToList();

            return data;
        }

        public List<EventExpenditureDTO> GetAllDeletedEventExpenditures()
        {
            var data = (from ex in _repository.GetAllDeletedEventExpenditures()
                        join e in _eventRepository.GetAll()
                            on ex.eventID equals e.eventID
                        select new
                        {
                            ex,
                            e
                        })
               .OrderByDescending(x => x.e.eventDate)
               .ThenByDescending(x => x.ex.amountSpent)
               .ToList()
               .Select((x, index) => new EventExpenditureDTO
               {
                   Counter = index + 1,
                   EventExpenditureId = x.ex.eventExpenditureID,
                   EventId = x.ex.eventID,
                   SpentAmount = x.ex.amountSpent,
                   FormattedSpentAmount = x.ex.amountSpent.ToString() + " €",
                   ExpenditureDate = x.e.eventDate,
                   FormattedExpenditureDate = x.e.eventDate.ToString("dd.MM.yyyy"),
                   Summary = x.ex.summary,
               })
               .ToList();

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

            else
            {
                return _repository.Update(data);                
            }
        }

        public decimal GetTotalAmountSpentByEvent(int eventId)
        {
            return _repository.GetAll()
                            .Where(x => x.eventID == eventId)
                            .Sum(x => (decimal?)x.amountSpent) ?? 0;
        }

        public decimal GetTotalAmountSpentByYear(int year)
        {
            return _repository.GetAll()
                            .Where(x => x.year == year)
                            .Sum(x => (decimal?)x.amountSpent) ?? 0;
        }
    }
}
