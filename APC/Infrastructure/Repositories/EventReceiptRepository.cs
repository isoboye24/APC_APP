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

        public List<EventReceipt> GetAll()
        {
            var data = _db.EVENT_RECEIPTS
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .OrderByDescending(x => x.monthID)
                .OrderByDescending(x => x.day)
                .OrderByDescending(x => x.caption)
                .ToList();

            return data
                .Select(x => EventReceipt.Rehydrate(
                    x.eventReceiptID,
                    x.eventID,
                    x.imagePath,
                    x.summary,
                    x.caption,
                    x.day,
                    x.monthID,
                    x.year,
                    x.receiptDate,
                    x.amountSpent
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENT_RECEIPTS.First(x => x.eventReceiptID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public EventReceipt GetById(int id)
        {
            var entity = _db.EVENT_RECEIPTS
                .Where(x => x.eventReceiptID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.eventReceiptID,
                    x.eventID,
                    x.imagePath,
                    x.summary,
                    x.caption,
                    x.day,
                    x.monthID,
                    x.year,
                    x.receiptDate,
                    x.amountSpent
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return EventReceipt.Rehydrate(
                    entity.eventReceiptID,
                    entity.eventID,
                    entity.imagePath,
                    entity.summary,
                    entity.caption,
                    entity.day,
                    entity.monthID,
                    entity.year,
                    entity.receiptDate,
                    entity.amountSpent
            );
        }

        public bool Insert(EventReceipt data)
        {
            _db.EVENT_RECEIPTS.Add(new EVENT_RECEIPTS
            {
                eventID = data.EventId,
                imagePath = data.ImagePath,
                summary = data.Summary,
                caption = data.Caption,
                day = data.Day,
                monthID = data.MonthId,
                year = data.Year,
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
            entity.day = data.Day;
            entity.monthID = data.MonthId;
            entity.year = data.Year;
            entity.receiptDate = data.ReceiptDate;
            entity.amountSpent = data.AmountSpent;

            _db.SaveChanges();
            return true;
        }
    }
}
