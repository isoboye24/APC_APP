using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.IO.RecyclableMemoryStreamManager;

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

        public List<TheEvents> GetAll()
        {
            var data = _db.EVENTS
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.eventDate.Year)
                .ThenByDescending(x => x.eventDate.Month)
                .ThenByDescending(x => x.eventDate.Day)
                .ThenBy(x => x.title)
                .ToList();

            return data
                .Select(x => TheEvents.Rehydrate(
                    x.eventID,
                    x.title,
                    x.summary,
                    x.coverImagePath,
                    x.eventDate
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENTS.First(x => x.eventID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public TheEvents GetById(int id)
        {
            var entity = _db.EVENTS
                .Where(x => x.eventID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.eventID,
                    x.title,
                    x.summary,
                    x.coverImagePath,
                    x.eventDate
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return TheEvents.Rehydrate(
                    entity.eventID,
                    entity.title,
                    entity.summary,
                    entity.coverImagePath,
                    entity.eventDate
            );
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
