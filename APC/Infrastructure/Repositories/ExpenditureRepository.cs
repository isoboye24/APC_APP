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
    public class ExpenditureRepository : IExpenditureRepository
    {
        private readonly APCEntities _db;
        public ExpenditureRepository(APCEntities db)
        {
            _db = db;
        }

        public int Count()
        {
            return _db.EXPENDITURE.Count(x => !x.isDeleted);
        }

        public bool Delete(int id)
        {
            var entity = _db.EXPENDITURE.First(x => x.expenditureID == id);
            entity.isDeleted = true;
            entity.deletedDate = DateTime.Today;
            _db.SaveChanges();
            return true;
        }

        public bool Exists(decimal spentAmount, string summary, DateTime date)
        {
            return _db.EXPENDITURE.Any(x => !x.isDeleted && x.amountSpent == spentAmount && x.summary == summary 
                                            && x.expenditureDate.Day == date.Day && x.expenditureDate.Month == date.Month
                                            && x.expenditureDate.Year == date.Year);
        }

        public List<Expenditure> GetAll()
        {
            var data = _db.EXPENDITURE
                .Where(x => !x.isDeleted)
                .OrderByDescending(x => x.year)
                .ThenByDescending(x => x.monthID)
                .ThenByDescending(x => x.day)
                .ThenByDescending(x => x.amountSpent)
                .ThenBy(x => x.summary)
                .ToList();

            return data
                .Select(x => Expenditure.Rehydrate(
                    x.expenditureID,
                    x.amountSpent,
                    x.summary,
                    x.expenditureDate
                ))
                .ToList();
        }

        public bool GetBack(int id)
        {
            var entity = _db.EXPENDITURE.First(x => x.expenditureID == id);
            entity.isDeleted = false;
            entity.deletedDate = null;
            _db.SaveChanges();
            return true;
        }

        public Expenditure GetById(int id)
        {
            var entity = _db.EXPENDITURE
                .Where(x => x.expenditureID == id && !x.isDeleted)
                .Select(x => new
                {
                    x.expenditureID,
                    x.amountSpent,
                    x.summary,
                    x.expenditureDate,
                })
                .FirstOrDefault();

            if (entity == null)
                return null;

            return Expenditure.Rehydrate(
                    entity.expenditureID,
                    entity.amountSpent,
                    entity.summary,
                    entity.expenditureDate
            );
        }

        public bool Insert(Expenditure data)
        {
            _db.EXPENDITURE.Add(new EXPENDITURE
            {
                amountSpent = data.AmountSpent,
                summary = data.Summary,
                expenditureDate = data.ExpenditureDate,
            });

            _db.SaveChanges();
            return true;
        }

        public bool PermanentDelete(int id)
        {
            var entity = _db.EXPENDITURE.FirstOrDefault(x => x.expenditureID == id);

            if (entity == null)
                return false;

            _db.EXPENDITURE.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Update(Expenditure data)
        {
            var entity = _db.EXPENDITURE.First(x => x.expenditureID == data.ExpenditureId);
            entity.amountSpent = data.AmountSpent;
            entity.summary = data.Summary;
            entity.expenditureDate = data.ExpenditureDate;

            _db.SaveChanges();
            return true;
        }
    }
}
