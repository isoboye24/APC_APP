using APC.Applications.DTO;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Collections.Generic;

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
            var data = (from t in _repository.GetAll()
                        join r in _routineRepository.GetAll() on t.dailiyRoutineID equals r.dailyRoutineID
                        where !t.isDeleted
                        select new
                        {
                            t.taskID,
                            t.categoryID,
                            c.categoryName,
                            r.routineDate,
                            t.dailiyRoutineID,
                            t.timeSpent,
                            t.summary
                        })
                        .ToList();

            return data.Select(x => new TaskDTO
            {
                Id = x.taskID,
                Category = x.categoryName,
                CategoryId = x.categoryID,
                DailyRoutineDate = x.routineDate,
                DailyRoutineId = x.dailiyRoutineID,
                TimeSpent = x.timeSpent,
                Summary = x.summary,
                Day = x.routineDate.Day,
                MonthID = x.routineDate.Month,
                Year = x.routineDate.Year,
                TimeInHoursAndMinutes = GeneralHelper.FormatTime(x.timeSpent)
            })
            .OrderByDescending(x => x.TimeSpent).ToList();


            return _repository.GetAll()
                .OrderBy(x => x.imageCaption)
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

            data.UpdateImageCaption(data.ImageCaption);
            data.UpdateSummary(data.Summary);
            data.UpdateImagePath(data.ImagePath);

            return _repository.Update(data);
        }
    }
}
