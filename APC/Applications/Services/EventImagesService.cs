using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<EventImages> GetAll()
            => _repository.GetAll();

        public bool GetBack(int id)
            => _repository.GetBack(id);

        public bool PermanentDelete(int id)
            => _repository.PermanentDelete(id);

        public bool Update(EventImages data)
        {
            var check = _repository.GetById(data.EventImagesId);
            if (check == null)
                throw new Exception("Event image not found");

            data.UpdateImageCaption(data.ImageCaption);
            data.UpdateSummary(data.Summary);
            data.UpdateImagePath(data.ImagePath);

            return _repository.Update(data);
        }
    }
}
