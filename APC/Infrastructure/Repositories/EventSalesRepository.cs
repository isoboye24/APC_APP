using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

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

        public bool Exists(int eventId, decimal amountSold, string summary, DateTime salesDate)
        {
            return _db.EVENT_SALES.Any(x => !x.isDeleted && x.eventID == eventId && x.amountSold == amountSold 
            && x.summary == summary && x.salesDate.Day == salesDate.Day && x.salesDate.Month == salesDate.Month 
            && x.salesDate.Year == salesDate.Year);
        }

        public IQueryable<EVENT_SALES> GetAll(int eventId)
        {
            return _db.EVENT_SALES.Where(x => !x.isDeleted && x.eventID == eventId);
        }
        
        public IQueryable<EVENT_SALES> GetAllDeletedEventSales()
        {
            return _db.EVENT_SALES.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENT_SALES.First(x => x.eventSalesID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<EVENT_SALES> GetById(int id)
        {
            return _db.EVENT_SALES.Where(x => !x.isDeleted && x.eventSalesID == id);
        }

        public bool Insert(EventSales data)
        {
            _db.EVENT_SALES.Add(new EVENT_SALES
            {
                eventID = data.EventId,
                amountSold = data.AmountSold,
                summary = data.Summary,
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
            entity.salesDate = data.SalesDate;

            _db.SaveChanges();
            return true;
        }
    }
}
