using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Applications.Services
{
    public class EventImagesService : IEventImagesService
    {
        private readonly IEventImagesRepository _repository;
        public EventImagesService(IEventImagesRepository repository)
        {
            _repository = repository;
        }

        public int Count()
            => _repository.Count();

        public bool Create(EventImages data)
        {
            if (_repository.Exists(data.EventId, data.ImagePath))
                throw new Exception("Event image already exists");

            return _repository.Insert(data);
        }

        public bool Delete(int id)
            => _repository.Delete(id);

        public List<EventImageDTO> GetAll()
        {
            return _repository.GetAll()
                .OrderBy(x => x.eventID)
                .ToList()
                .Select((x, index) => new EventImageDTO
                {
                    Counter = index + 1,
                    EventImageId = x.eventImageID,
                    EventId = x.eventID,
                    Summary = x.summary,
                    ImagePath = x.imagePath,
                    ImageCaption = x.imageCaption,
                })
                .ToList();
        }
        
        public List<EventImageDTO> GetByEvent(int eventId)
        {
            return _repository.GetAll()
                .Where(x => x.eventID == eventId)
                .OrderBy(x => x.imageCaption)
                .ToList()
                .Select((x, index) => new EventImageDTO
                {
                    Counter = index + 1,
                    EventImageId = x.eventImageID,
                    EventId = x.eventID,
                    Summary = x.summary,
                    ImagePath = x.imagePath,
                    ImageCaption = x.imageCaption,
                })
                .ToList();
        }

        public List<EventImageDTO> GetAllDeletedEventImages()
        {
            return _repository.GetAllDeletedEventImages()
                .OrderBy(x => x.imageCaption)
                .ToList()
                .Select((x, index) => new EventImageDTO
                {
                    Counter = index + 1,
                    EventImageId = x.eventImageID,
                    EventId = x.eventID,
                    Summary = x.summary,
                    ImagePath = x.imagePath,
                    ImageCaption = x.imageCaption,
                })
                .ToList();
        }


        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(EventImages data)
        {
            var check = _repository.GetById(data.EventImagesId);
            if (check == null)
                throw new Exception("Event image not found");

            else
            {
                return _repository.Update(data);                
            }
        }

        public int GetEventImagesByEventCount(int eventId)
        {
            return _repository.GetAll()
                                .Where(x => x.eventID == eventId)
                                .Count();
        }
        
        public int GetAllEventImagesCount()
        {
            return _repository.GetAll()                                
                                .Count();
        }
    }
}
