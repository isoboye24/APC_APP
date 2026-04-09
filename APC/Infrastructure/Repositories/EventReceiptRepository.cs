using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class EventReceiptRepository : IEventReceiptRepository
    {
        private readonly APCEntities _db;
        public EventReceiptRepository(APCEntities db)
        {
            _db = db;
        }
        public int Count()
        {
            return _db.EVENT_RECEIPTS.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.EVENT_RECEIPTS.First(x => x.eventReceiptID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(int eventId, string imagePath)
        {
            return _db.EVENT_RECEIPTS.Any(x => !x.isDeleted && x.eventID == eventId && x.imagePath == imagePath);
        }

        public IQueryable<EVENT_RECEIPTS> GetAll(int eventId)
        {
            return _db.EVENT_RECEIPTS.Where(x => !x.isDeleted && x.eventID == eventId);
        }

        public IQueryable<EVENT_RECEIPTS> GetAllDeletedEventReceipts()
        {
            return _db.EVENT_RECEIPTS.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENT_RECEIPTS.First(x => x.eventReceiptID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<EVENT_RECEIPTS> GetById(int id)
        {
            return _db.EVENT_RECEIPTS.Where(x => !x.isDeleted && x.eventReceiptID == id);
        }

        public bool Insert(EventReceipt data)
        {
            _db.EVENT_RECEIPTS.Add(new EVENT_RECEIPTS
            {
                eventID = data.EventId,
                imagePath = data.ImagePath,
                summary = data.Summary,
                caption = data.Caption,
                receiptDate = data.ReceiptDate,
                amountSpent = data.AmountSpent,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.EVENT_RECEIPTS.FirstOrDefault(x => x.eventReceiptID == id);

            if (entity == null)
                return false;

            _db.EVENT_RECEIPTS.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(EventReceipt data)
        {
            var entity = _db.EVENT_RECEIPTS.First(x => x.eventReceiptID == data.EventReceiptId);
            entity.eventID = data.EventId;
            entity.summary = data.Summary;
            entity.imagePath = data.ImagePath;
            entity.caption = data.Caption;
            entity.receiptDate = data.ReceiptDate;
            entity.amountSpent = data.AmountSpent;

            _db.SaveChanges();
            return true;
        }
    }
}
