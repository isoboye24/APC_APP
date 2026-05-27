using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _repository;
        private readonly IEventSalesRepository _eventSalesRepository;
        private readonly IEventExpenditureRepository _eventExpenditureRepository;
        public EventsService(IEventsRepository repository, IEventSalesRepository eventSalesRepository, IEventExpenditureRepository eventExpenditureRepository)
        {
            _repository = repository;
            _eventSalesRepository = eventSalesRepository;
            _eventExpenditureRepository = eventExpenditureRepository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(TheEvents data)
        {
            if (_repository.Exists(data.Title, data.EventsDate))
                throw new Exception("Event already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<EventDTO> GetAll()
        {
            return _repository.GetAll()
                .Select(x => new EventDTO
                {
                    EventsId = x.eventID,
                    Title = x.title,
                    Summary = x.summary,
                    CoverImagePath = x.coverImagePath,
                    EventsDate = x.eventDate,
                    FormattedEventsDate = x.eventDate.ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.EventsDate.Year)
                .ThenByDescending(x => x.EventsDate.Month)
                .ThenByDescending(x => x.EventsDate.Day)
                .ThenBy(x => x.Title)
                .ToList();
        }
        
        public List<EventDTO> GetAnnualEvents(int year)
        {
            return _repository.GetAll().Where(x => x.year == year)
                .Select(x => new EventDTO
                {
                    EventsId = x.eventID,
                    Title = x.title,
                    Summary = x.summary,
                    CoverImagePath = x.coverImagePath,
                    EventsDate = x.eventDate,
                    FormattedEventsDate = x.eventDate.ToString("dd.MM.yyyy"),
                })
                .OrderByDescending(x => x.EventsDate.Year)
                .ThenByDescending(x => x.EventsDate.Month)
                .ThenByDescending(x => x.EventsDate.Day)
                .ThenBy(x => x.Title)
                .ToList();
        }

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(TheEvents data)
        {
            var check = _repository.GetById(data.EventsId);
            if (check == null)
                throw new Exception("Event not found");

            data.UpdateSummary(data.Summary);
            data.UpdateDate(data.EventsDate);
            data.UpdateTitle(data.Title);
            data.UpdateImagePath(data.CoverImagePath);

            return _repository.Update(data);
        }
    }
}
