using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class EventImagesRepository : IEventImagesRepository
    {
        private readonly APCEntities _db;
        public EventImagesRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.EVENT_IMAGE.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.EVENT_IMAGE.First(x => x.eventImageID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(int eventId, string imagePath)
        {
            return _db.EVENT_IMAGE.Any(x => !x.isDeleted && x.eventID == eventId && x.imagePath == imagePath);
        }

        public IQueryable<EVENT_IMAGE> GetAll()
        {
            return _db.EVENT_IMAGE.Where(x => !x.isDeleted);
        }
        
        public IQueryable<EVENT_IMAGE> GetAllDeletedEventImages()
        {
            return _db.EVENT_IMAGE.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENT_IMAGE.First(x => x.eventImageID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public EventImages GetById(int id)
        {
            var entity = _db.EVENT_IMAGE
                .Where(x => x.eventImageID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.eventImageID,
                    x.eventID,
                    x.summary,
                    x.imagePath,
                    x.imageCaption
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return EventImages.Rehydrate(
                    entity.eventImageID,
                    entity.eventID,
                    entity.summary,
                    entity.imagePath,
                    entity.imageCaption
            );
        }

        public bool Insert(EventImages data)
        {
            _db.EVENT_IMAGE.Add(new EVENT_IMAGE
            {
                eventID = data.EventId,
                summary = data.Summary,
                imagePath = data.ImagePath,
                imageCaption = data.ImageCaption,
                
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.EVENT_IMAGE.FirstOrDefault(x => x.eventImageID == id);

            if (entity == null)
                return false;

            _db.EVENT_IMAGE.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(EventImages data)
        {
            var entity = _db.EVENT_IMAGE.First(x => x.eventImageID == data.EventImagesId);
            entity.eventID = data.EventId;
            entity.summary = data.Summary;
            entity.imagePath = data.ImagePath;
            entity.imageCaption = data.ImageCaption;

            _db.SaveChanges();
            return true;
        }
    }
}
