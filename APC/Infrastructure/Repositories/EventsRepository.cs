using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly APCEntities _db;
        public EventsRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.EVENTS.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.EVENTS.First(x => x.eventID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(string title, DateTime eventsDate)
        {
            return _db.EVENTS.Any(x => !x.isDeleted && x.title == title && x.eventDate.Day == eventsDate.Day 
            && x.eventDate.Month == eventsDate.Month && x.eventDate.Year == eventsDate.Year);
        }

        public IQueryable<EVENTS> GetAll()
        {
            return _db.EVENTS.Where(x => !x.isDeleted);
        }
        
        public IQueryable<EVENTS> GetAllDeletedEvents()
        {
            return _db.EVENTS.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENTS.First(x => x.eventID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<EVENTS> GetById(int id)
        {
            return _db.EVENTS.Where(x => !x.isDeleted && x.eventID == id);
        }

        public bool Insert(TheEvents data)
        {
            _db.EVENTS.Add(new EVENTS
            {
                title = data.Title,
                summary = data.Summary,
                coverImagePath = data.CoverImagePath,
                eventDate = data.EventsDate,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.EVENTS.FirstOrDefault(x => x.eventID == id);

            if (entity == null)
                return false;

            _db.EVENTS.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(TheEvents data)
        {
            var entity = _db.EVENTS.First(x => x.eventID == data.EventsId);
            entity.title = data.Title;
            entity.summary = data.Summary;
            entity.coverImagePath = data.CoverImagePath;
            entity.eventDate = data.EventsDate;

            _db.SaveChanges();
            return true;
        }
    }
}
