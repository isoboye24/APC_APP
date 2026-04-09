using APC.DAL;
using APC.Domain.Entities;
using APC.Applications.Interfaces;
using System;
using System.Linq;

namespace APC.Infrastructure.Repositories
{
    public class EventExpenditureRepository : IEventExpenditureRepository
    {
        private readonly APCEntities _db;
        public EventExpenditureRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.EVENT_EXPENDITURE.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.EVENT_EXPENDITURE.First(x => x.eventExpenditureID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(int eventId, decimal spentAmount, string summary, DateTime date)
        {
            return _db.EVENT_EXPENDITURE.Any(x => !x.isDeleted && x.eventID == eventId && x.amountSpent == spentAmount 
                                        && x.summary == summary && x.expenditureDate.Day == date.Day && x.expenditureDate.Month == date.Month
                                        && x.expenditureDate.Year == date.Year);
        }

        public IQueryable<EVENT_EXPENDITURE> GetAll(int eventId)
        {
            return _db.EVENT_EXPENDITURE.Where(x => !x.isDeleted && x.eventID == eventId);
        }
        
        public IQueryable<EVENT_EXPENDITURE> GetAllDeletedEventExpenditures()
        {
            return _db.EVENT_EXPENDITURE.Where(x => x.isDeleted);
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENT_EXPENDITURE.First(x => x.eventExpenditureID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public IQueryable<EVENT_EXPENDITURE> GetById(int id)
        {
            return _db.EVENT_EXPENDITURE.Where(x => !x.isDeleted && x.eventExpenditureID == id);
        }

        public bool Insert(EventExpenditure data)
        {
            _db.EVENT_EXPENDITURE.Add(new EVENT_EXPENDITURE
            {
                eventID = data.EventId,
                amountSpent = data.SpentAmount,
                summary = data.Summary,
                expenditureDate = data.ExpenditureDate,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.EVENT_EXPENDITURE.FirstOrDefault(x => x.eventExpenditureID == id);

            if (entity == null)
                return false;

            _db.EVENT_EXPENDITURE.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(EventExpenditure data)
        {
            var entity = _db.EVENT_EXPENDITURE.First(x => x.eventExpenditureID == data.EventExpenditureId);
            entity.eventID = data.EventId;
            entity.amountSpent = data.SpentAmount;
            entity.summary = data.Summary;

            _db.SaveChanges();
            return true;
        }
    }
}
