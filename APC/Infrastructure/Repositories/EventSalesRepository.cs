using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.Infrastructure.Repositories
{
    public class EventSalesRepository : IEventSalesRepository
    {
        private readonly APCEntities _db;
        public EventSalesRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.EVENT_SALES.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.EVENT_SALES.First(x => x.eventSalesID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(int eventId, decimal amountSold, string summary, int day, int monthId, int year)
        {
            return _db.EVENT_SALES.Any(x => !x.isDeleted && x.eventID == eventId && x.amountSold == amountSold 
            && x.summary == summary && x.salesDate.Day == day && x.salesDate.Month == monthId && x.salesDate.Year == year);
        }

        public List<EventSales> GetAll()
        {
            var data = _db.EVENT_SALES
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .ThenByDescending(x => x.monthID)
                .ThenByDescending(x => x.day)
                .ThenByDescending(x => x.amountSold)
                .ToList();

            return data
                .Select(x => EventSales.Rehydrate(
                    x.eventSalesID,
                    x.eventID,
                    x.amountSold,
                    x.summary,
                    x.day,
                    x.monthID,
                    x.year,
                    x.salesDate
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENT_SALES.First(x => x.eventSalesID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public EventSales GetById(int id)
        {
            var entity = _db.EVENT_SALES
                .Where(x => x.eventSalesID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.eventSalesID,
                    x.eventID,
                    x.amountSold,
                    x.summary,
                    x.day,
                    x.monthID,
                    x.year,
                    x.salesDate
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return EventSales.Rehydrate(
                    entity.eventSalesID,
                    entity.eventID,
                    entity.amountSold,
                    entity.summary,
                    entity.day,
                    entity.monthID,
                    entity.year,
                    entity.salesDate
            );
        }

        public bool Insert(EventSales data)
        {
            _db.EVENT_SALES.Add(new EVENT_SALES
            {
                eventID = data.EventId,
                amountSold = data.AmountSold,
                summary = data.Summary,
                day = data.Day,
                monthID = data.MonthId,
                year = data.Year,
                salesDate = data.SalesDate,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.EVENT_SALES.FirstOrDefault(x => x.eventSalesID == id);

            if (entity == null)
                return false;

            _db.EVENT_SALES.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(EventSales data)
        {
            var entity = _db.EVENT_SALES.First(x => x.eventSalesID == data.EventSalesId);
            entity.eventID = data.EventId;
            entity.amountSold = data.AmountSold;
            entity.summary = data.Summary;
            entity.day = data.Day;
            entity.monthID = data.MonthId;
            entity.year = data.Year;
            entity.salesDate = data.SalesDate;

            _db.SaveChanges();
            return true;
        }
    }
}
