using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class EventSalesService : IEventSalesService
    {
        private readonly IEventSalesRepository _repository;
        private readonly IEventsRepository _eventRepository;

        public EventSalesService(IEventSalesRepository repository, IEventsRepository eventRepository)
        {
            _repository = repository;
            _eventRepository = eventRepository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(EventSales data)
        {
            if (_repository.Exists(data.EventId, data.AmountSold, data.Summary, data.SalesDate))
                throw new Exception("Event sales already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<EventSalesDTO> GetByEvent(int eventId)
        {
            var data = (from es in _repository.GetAll().Where(x => x.eventID == eventId)
                        join e in _eventRepository.GetAll()
                            on es.eventID equals e.eventID
                        select new
                        {
                            es,
                            e
                        })
                .OrderByDescending(x => x.e.eventDate)
                .ThenByDescending(x => x.es.amountSold)
                .ToList()
                .Select((x, index) => new EventSalesDTO
                {
                    Counter = index + 1,
                    EventSalesId = x.es.eventSalesID,
                    EventId = x.es.eventID,
                    AmountSold = x.es.amountSold,
                    FormattedAmountSold = x.es.amountSold.ToString() + " €",
                    SalesDate = x.e.eventDate,
                    Summary = x.es.summary,
                    FormattedSalesDate = x.e.eventDate.ToString("dd.MM.yyyy"),
                })
                .ToList();

            return data;

           }

        public List<EventSalesDTO> GetAll()
        {
            var data = (from es in _repository.GetAll()
                        join e in _eventRepository.GetAll()
                            on es.eventID equals e.eventID
                        select new
                        {
                            es,
                            e
                        })
                .OrderByDescending(x => x.e.eventDate)
                .ThenByDescending(x => x.es.amountSold)
                .ToList()
                .Select((x, index) => new EventSalesDTO
                {
                    Counter = index + 1,
                    EventSalesId = x.es.eventSalesID,
                    EventId = x.es.eventID,
                    AmountSold = x.es.amountSold,
                    FormattedAmountSold = x.es.amountSold.ToString() + " €",
                    SalesDate = x.e.eventDate,
                    Summary = x.es.summary,
                    FormattedSalesDate = x.e.eventDate.ToString("dd.MM.yyyy"),
                })
                .ToList();

            return data;
        }

        public List<EventSalesDTO> GetAllDeletedEventSales()
        {
            var data = (from es in _repository.GetAllDeletedEventSales()
                        join e in _eventRepository.GetAll()
                            on es.eventID equals e.eventID
                        select new
                        {
                            es,
                            e
                        })
                .OrderByDescending(x => x.e.eventDate)
                .ThenByDescending(x => x.es.amountSold)
                .ToList()
                .Select((x, index) => new EventSalesDTO
                {
                    Counter = index + 1,
                    EventSalesId = x.es.eventSalesID,
                    EventId = x.es.eventID,
                    AmountSold = x.es.amountSold,
                    FormattedAmountSold = x.es.amountSold.ToString() + " €",
                    SalesDate = x.e.eventDate,
                    Summary = x.es.summary,
                    FormattedSalesDate = x.e.eventDate.ToString("dd.MM.yyyy"),
                })
                .ToList();

            return data;
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(EventSales data)
        {
            var check = _repository.GetById(data.EventSalesId);
            if (check == null)
                throw new Exception("Event sales not found");

            data.UpdateSummary(data.Summary);
            data.UpdateDate(data.SalesDate);
            data.UpdateAmountSold(data.AmountSold);

            return _repository.Update(data);
        }

        public decimal GetSalesAmountByEvent(int eventId)
        {
            return _repository.GetAll()
                                .Where(x => x.eventID == eventId)
                                .Sum(x => (decimal?)x.amountSold) ?? 0;
        }

        public decimal GetSalesAmountByYear(int year)
        {
            return _repository.GetAll()                                
                                .Sum(x => (decimal?)x.amountSold) ?? 0;
        }
    }
}
