using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;

namespace APC.Applications.Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _repository;
        public EventsService(IEventsRepository repository)
        {
            _repository = repository;
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

        public List<TheEvents> GetAll()
            => _repository.GetAll();

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
