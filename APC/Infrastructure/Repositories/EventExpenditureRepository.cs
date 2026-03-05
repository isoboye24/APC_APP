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

        public bool Exists(int eventId, decimal spentAmount, string summary, int day, int monthId, int year)
        {
            return _db.EVENT_EXPENDITURE.Any(x => !x.isDeleted && x.eventID == eventId && x.amountSpent == spentAmount 
                                        && x.summary == summary && x.expenditureDate.Day == day && x.expenditureDate.Month == monthId
                                        && x.expenditureDate.Year == year);
        }

        public List<EventExpenditure> GetAll()
        {
            var data = _db.EVENT_EXPENDITURE
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .OrderByDescending(x => x.monthID)
                .OrderByDescending(x => x.day)
                .OrderByDescending(x => x.amountSpent)
                .ThenBy(x => x.summary)
                .ToList();

            return data
                .Select(x => EventExpenditure.Rehydrate(
                    x.eventExpenditureID,
                    x.eventID,
                    x.amountSpent,
                    x.expenditureDate,
                    x.summary,
                    x.day,
                    x.monthID,
                    x.year
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
                    x.day,
                    x.monthID,
                    x.year
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return EventExpenditure.Rehydrate(
                    entity.eventExpenditureID,
                    entity.eventID,
                    entity.amountSpent,
                    entity.expenditureDate,
                    entity.summary,
                    entity.day,
                    entity.monthID,
                    entity.year
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
                day = data.Day,
                monthID = data.MonthId,
                year = data.Year,
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
            entity.day = data.Day;
            entity.monthID = data.MonthId;
            entity.year = data.Year;

            _db.SaveChanges();
            return true;
        }
    }
}
