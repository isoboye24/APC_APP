using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class EventSalesService : IEventSalesService
    {
        private readonly IEventSalesRepository _repository;
        private readonly EventsRepository _eventRepository;

        public EventSalesService(IEventSalesRepository repository, EventsRepository eventRepository)
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
                        join e in _eventRepository.GetAll() on es.eventID equals e.eventID
                        select new EventSalesDTO
                        {
                            EventSalesId = es.eventSalesID,
                            EventId = es.eventID,
                            AmountSold = es.amountSold,
                            SalesDate = e.eventDate,
                            Summary = es.summary,

                        }).OrderByDescending(x => x.SalesDate.Year).ThenByDescending(x => x.SalesDate.Month).ThenByDescending(x => x.SalesDate.Day).ThenByDescending(x => x.AmountSold).ToList();

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

        public decimal GetTotalSalesByEvent(int eventId)
        {
            return _repository.GetAll()
                                .Where(x => x.eventID == eventId)
                                .Sum(x => (decimal?)x.amountSold) ?? 0;
        }

        public decimal GetTotalSalesByYear(int year)
        {
            return _repository.GetAll()                                
                                .Sum(x => (decimal?)x.amountSold) ?? 0;
        }
    }
}
