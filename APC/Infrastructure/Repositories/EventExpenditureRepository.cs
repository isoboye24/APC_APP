using APC.DAL;
using APC.Domain.Entities;
using APC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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

        public List<EventExpenditure> GetAll()
        {
            var data = _db.EVENT_EXPENDITURE
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .ThenByDescending(x => x.monthID)
                .ThenByDescending(x => x.day)
                .ThenByDescending(x => x.amountSpent)
                .ThenBy(x => x.summary)
                .ToList();

            return data
                .Select(x => EventExpenditure.Rehydrate(
                    x.eventExpenditureID,
                    x.eventID,
                    x.amountSpent,
                    x.expenditureDate,
                    x.summary
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.EVENT_EXPENDITURE.First(x => x.eventExpenditureID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public EventExpenditure GetById(int id)
        {
            var entity = _db.EVENT_EXPENDITURE
                .Where(x => x.eventExpenditureID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.eventExpenditureID,
                    x.eventID,
                    x.amountSpent,
                    x.expenditureDate,
                    x.summary,
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return EventExpenditure.Rehydrate(
                    entity.eventExpenditureID,
                    entity.eventID,
                    entity.amountSpent,
                    entity.expenditureDate,
                    entity.summary
            );
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
